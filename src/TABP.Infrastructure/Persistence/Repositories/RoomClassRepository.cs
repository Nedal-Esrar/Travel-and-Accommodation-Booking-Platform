using System.Linq.Expressions;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;
using TABP.Domain.Models;
using TABP.Infrastructure.Persistence.DbContexts;
using TABP.Infrastructure.Persistence.Extensions;
using TABP.Infrastructure.Persistence.Helpers;

namespace TABP.Infrastructure.Persistence.Repositories;

public class RoomClassRepository(HotelBookingDbContext context) : IRoomClassRepository
{
  public async Task<bool> ExistsAsync(Expression<Func<RoomClass, bool>> predicate,
                                      CancellationToken cancellationToken = default)
  {
    return await context.RoomClasses.AnyAsync(predicate, cancellationToken);
  }
  
  public async Task<RoomClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    return await context.RoomClasses
      .FirstOrDefaultAsync(rc => rc.Id == id, cancellationToken);
  }

  public async Task<RoomClass> CreateAsync(RoomClass roomClass, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(roomClass);

    var createdRoomClass = await context.RoomClasses.AddAsync(roomClass, cancellationToken);

    return createdRoomClass.Entity;
  }

  public async Task UpdateAsync(RoomClass roomClass, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(roomClass);

    if (!await ExistsAsync(rc => rc.Id == roomClass.Id, cancellationToken))
      throw new NotFoundException(RoomClassMessages.NotFound);

    context.RoomClasses.Update(roomClass);
  }

  public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    if (!await context.RoomClasses.AnyAsync(r => r.Id == id, cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }
    
    var entity = context.ChangeTracker.Entries<RoomClass>()
                   .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                 ?? new RoomClass { Id = id };

    context.RoomClasses.Remove(entity);
  }

  public async Task<PaginatedList<RoomClass>> GetAsync(
    Query<RoomClass> query, 
    bool includeGallery = false,
    CancellationToken cancellationToken = default)
  {
    var currentDateTime = DateTime.UtcNow;
    
    var queryable = context.RoomClasses
      .Include(rc => rc.Discounts
        .Where(d => currentDateTime >= d.StartDateUtc && currentDateTime < d.EndDateUtc))
      .Include(rc => rc.Amenities)
      .AsSplitQuery()
      .Where(query.Filter)
      .Sort(SortingExpressions.GetRoomClassSortExpression(query.SortColumn), query.SortOrder);

    var requestedPage = queryable.GetPage(query.PageNumber, query.PageSize);

    IEnumerable<RoomClass> itemsToReturn;

    if (includeGallery)
    {
      itemsToReturn = (await requestedPage.Select(rc => new
      {
        RoomClass = rc,
        Gallery = context.Images
          .Where(i => i.EntityId == rc.Id && i.Type == ImageType.Gallery)
          .ToList()
      }).AsNoTracking().ToListAsync(cancellationToken))
      .Select(rc =>
      {
        rc.RoomClass.Gallery = rc.Gallery;

        return rc.RoomClass;
      });
    }
    else
    {
      itemsToReturn = await requestedPage
        .AsNoTracking()
        .ToListAsync(cancellationToken);
    }
    
    return new PaginatedList<RoomClass>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }

  public async Task<IEnumerable<RoomClass>> GetFeaturedDealsInDifferentHotelsAsync(int count,
    CancellationToken cancellationToken = default)
  {
    var currentDateTime = DateTime.UtcNow;

    var featuredDeals = await (from rcd in (from rc in (
            from rc in context.RoomClasses.ToLinqToDB()
            join d in context.Discounts
                .Where(d => d.StartDateUtc <= currentDateTime && d.EndDateUtc > currentDateTime)
                .ToLinqToDB()
              on rc.Id equals d.RoomClassId
            select new
            {
              RoomClass = rc,
              ActiveDiscount = d,
              Rank = LinqToDB.AnalyticFunctions.RowNumber(LinqToDB.Sql.Ext)
                .Over()
                .PartitionBy(rc.HotelId)
                .OrderByDesc(d.Percentage)
                .ThenBy(rc.PricePerNight)
                .ToValue()
            })
          where rc.Rank <= 1
          select new { rc.RoomClass, rc.ActiveDiscount }).Take(count)
        join h in context.Hotels.Include(h => h.City).ToLinqToDB() 
          on rcd.RoomClass.HotelId equals h.Id
        join i in context.Images.ToLinqToDB() 
          on h.Id equals i.EntityId into images
        from img in images.DefaultIfEmpty()
        where img.Type == ImageType.Thumbnail
        select new { rcd.RoomClass, rcd.ActiveDiscount, Hotel = h, Thumbnail = img })
      .ToListAsync(cancellationToken: cancellationToken);

    return featuredDeals.Select(deal =>
    {
      deal.Hotel.Thumbnail = deal.Thumbnail;

      deal.RoomClass.Discounts.Add(deal.ActiveDiscount);

      deal.RoomClass.Hotel = deal.Hotel;

      return deal.RoomClass;
    });
  }
}