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

public class CityRepository(HotelBookingDbContext context) : ICityRepository
{
  public async Task<bool> ExistsAsync(Expression<Func<City, bool>> predicate,
                                      CancellationToken cancellationToken = default)
  {
    return await context.Cities.AnyAsync(predicate, cancellationToken);
  }
  public async Task<PaginatedList<CityForManagement>> GetForManagementAsync(
    Query<City> query,
    CancellationToken cancellationToken = default)
  {
    var queryable = context.Cities
      .Where(query.Filter)
      .Sort(SortingExpressions.GetCitySortExpression(query.SortColumn), query.SortOrder)
      .Select(c => new CityForManagement
      {
        Id = c.Id,
        Country = c.Country,
        Name = c.Name,
        PostOffice = c.PostOffice,
        NumberOfHotels = c.Hotels.Count,
        CreatedAtUtc = c.CreatedAtUtc,
        ModifiedAtUtc = c.ModifiedAtUtc
      });

    var itemsToReturn = await queryable
      .GetPage(query.PageNumber, query.PageSize)
      .ToListAsync(cancellationToken);

    return new PaginatedList<CityForManagement>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }

  public async Task<City?> GetByIdAsync(Guid id,
    CancellationToken cancellationToken = default)
  {
    return await context.Cities
      .FindAsync([id], cancellationToken);
  }

  public async Task<City> CreateAsync(City city,
    CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(city);
    
    var createdCity = await context.Cities.AddAsync(city, cancellationToken);

    return createdCity.Entity;
  }

  public async Task DeleteAsync(Guid id,
    CancellationToken cancellationToken = default)
  {
    if (!await context.Cities.AnyAsync(r => r.Id == id, cancellationToken))
    {
      throw new NotFoundException(CityMessages.NotFound);
    }
    
    var entity = context.ChangeTracker.Entries<City>()
                   .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                 ?? new City { Id = id };

    context.Cities.Remove(entity);
  }

  public async Task UpdateAsync(City city,
    CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(city);
    
    if (!await context.Cities.AnyAsync(c => c.Id == city.Id, cancellationToken))
      throw new NotFoundException(CityMessages.NotFound);

    context.Cities.Update(city);
  }

  public async Task<IEnumerable<City>> GetMostVisitedAsync(int count,
    CancellationToken cancellationToken = default)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

    var mostVisitedCityIdsCte = LinqToDB.LinqExtensions
      .AsCte(context.Bookings
        .GroupBy(b => b.Hotel.CityId)
        .OrderByDescending(g => g.Count())
        .Take(count).Select(g => g.Key));

    var mostVisitedCitiesQuery = 
      from c in context.Cities.ToLinqToDB()
      join cId in mostVisitedCityIdsCte on c.Id equals cId
      join i in context.Images.ToLinqToDB()
        on c.Id equals i.EntityId into images
      from img in images.DefaultIfEmpty()
      where img.Type == ImageType.Thumbnail
      select new { City = c, Thumbnail = img };

    var mostVisitedCities = await mostVisitedCitiesQuery
      .AsNoTracking()
      .ToListAsync(cancellationToken);
    
    return mostVisitedCities.Select(c =>
    {
      c.City.Thumbnail = c.Thumbnail;

      return c.City;
    });;
  }
}