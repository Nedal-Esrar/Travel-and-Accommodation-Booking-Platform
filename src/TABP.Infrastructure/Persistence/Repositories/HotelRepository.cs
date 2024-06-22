using System.Linq.Expressions;
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

public class HotelRepository(HotelBookingDbContext context) : IHotelRepository
{
  public async Task<bool> ExistsAsync(Expression<Func<Hotel, bool>> predicate,
                                      CancellationToken cancellationToken = default)
  {
    return await context.Hotels.AnyAsync(predicate, cancellationToken);
  }
  
  public async Task<PaginatedList<HotelForManagement>> GetForManagementAsync(
    Query<Hotel> query,
    CancellationToken cancellationToken = default)
  {
    var queryable = context.Hotels
      .Where(query.Filter)
      .Sort(SortingExpressions.GetHotelSortExpression(query.SortColumn), query.SortOrder)
      .Select(h => new HotelForManagement
      {
        Id = h.Id,
        Name = h.Name,
        StarRating = h.StarRating,
        Owner = h.Owner,
        NumberOfRooms = h.RoomClasses.SelectMany(rc => rc.Rooms).Count(),
        CreatedAtUtc = h.CreatedAtUtc,
        ModifiedAtUtc = h.ModifiedAtUtc
      });

    var itemsToReturn = await queryable
      .GetPage(query.PageNumber, query.PageSize)
      .ToListAsync(cancellationToken);

    return new PaginatedList<HotelForManagement>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }

  public async Task<Hotel?> GetByIdAsync(Guid id, 
    bool includeCity = false,
    bool includeThumbnail = false,
    bool includeGallery = false,
    CancellationToken cancellationToken = default)
  {
    var query = context.Hotels
      .Where(h => h.Id == id);

    if (includeCity)
    {
      query.Include(h => h.City);
    }

    var hotel = await query.FirstOrDefaultAsync(cancellationToken);

    if (hotel is null)
    {
      return hotel;
    }

    if (includeThumbnail)
    {
      hotel.Thumbnail = await context.Images.FirstOrDefaultAsync(
        i => i.EntityId == hotel.Id && i.Type == ImageType.Thumbnail, cancellationToken);
    }
    
    if (includeGallery)
    {
      hotel.Gallery = await context.Images.Where(
          i => i.EntityId == hotel.Id && i.Type == ImageType.Gallery)
        .ToListAsync(cancellationToken);
    }

    return hotel;
  }

  public async Task<Hotel> CreateAsync(Hotel hotel, CancellationToken cancellationToken = default)
  {
    var createdHotel = await context.Hotels.AddAsync(hotel, cancellationToken);

    return createdHotel.Entity;
  }

  public async Task UpdateAsync(Hotel hotel, CancellationToken cancellationToken = default)
  {
    if (!await context.Hotels.AnyAsync(h => h.Id == hotel.Id, cancellationToken))
      throw new NotFoundException(HotelMessages.NotFound);

    context.Hotels.Update(hotel);
  }

  public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    if (!await context.Hotels.AnyAsync(r => r.Id == id, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    var hotelEntity = context.ChangeTracker.Entries<Hotel>()
                       .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                     ?? new Hotel { Id = id };

    context.Hotels.Remove(hotelEntity);
  }

  public async Task<PaginatedList<HotelSearchResult>> GetForSearchAsync(Query<Hotel> query, CancellationToken cancellationToken = default)
  {
    var queryable = context.Hotels
      .Where(query.Filter)
      .Select(h => new HotelSearchResult
      {
        Id = h.Id,
        Name = h.Name,
        StarRating = h.StarRating,
        ReviewsRating = h.ReviewsRating,
        BriefDescription = h.BriefDescription,
        PricePerNightStartingAt = h.RoomClasses.Min(rc => rc.PricePerNight)
      })
      .Sort(SortingExpressions.GetHotelSearchSortExpression(query.SortColumn), query.SortOrder);

    var requestedPage = queryable.GetPage(
      query.PageNumber, query.PageSize);
    
    var itemsToReturn = (await requestedPage.Select(h => new
      {
        Hotel = h,
        Thumbnail = context.Images
          .Where(i => i.EntityId == h.Id && i.Type == ImageType.Thumbnail)
          .ToList()
      }).ToListAsync(cancellationToken))
      .Select(h =>
      {
        h.Hotel.Thumbnail = h.Thumbnail.FirstOrDefault();
    
        return h.Hotel;
      });

    return new PaginatedList<HotelSearchResult>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }

  public async Task UpdateReviewById(Guid id, double newRating, CancellationToken cancellationToken = default)
  {
    if (!await context.Hotels.AnyAsync(h => h.Id == id, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }
    
    var entity = context.ChangeTracker.Entries<Hotel>()
                   .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                 ?? new Hotel { Id = id };

    entity.ReviewsRating = newRating;

    context.Hotels.Update(entity);
  }
}

