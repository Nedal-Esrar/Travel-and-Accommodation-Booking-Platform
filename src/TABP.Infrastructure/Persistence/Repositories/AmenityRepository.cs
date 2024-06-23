using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;
using TABP.Domain.Models;
using TABP.Infrastructure.Persistence.DbContexts;
using TABP.Infrastructure.Persistence.Extensions;
using TABP.Infrastructure.Persistence.Helpers;

namespace TABP.Infrastructure.Persistence.Repositories;

public class AmenityRepository(HotelBookingDbContext context) : IAmenityRepository
{
  public async Task<Amenity?> GetByIdAsync(Guid id,
    CancellationToken cancellationToken = default)
  {
    return await context.Amenities
      .FindAsync([id], cancellationToken);
  }

  public async Task<bool> ExistsAsync(Expression<Func<Amenity, bool>> predicate, 
                                      CancellationToken cancellationToken = default)
  {
    return await context.Amenities.AnyAsync(predicate, cancellationToken);
  }

  public async Task<Amenity> CreateAsync(Amenity amenity,
    CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(amenity);
    
    var createdAmenity = await context.Amenities
      .AddAsync(amenity, cancellationToken);

    return createdAmenity.Entity;
  }

  public async Task UpdateAsync(Amenity amenity, 
    CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(amenity);
    
    if (!await context.Amenities.AnyAsync(
          a => a.Id == amenity.Id, cancellationToken))
      throw new NotFoundException(AmenityMessages.WithIdNotFound);

    context.Amenities.Update(amenity);
  }

  public async Task<PaginatedList<Amenity>> GetAsync(
    Query<Amenity> query,
    CancellationToken cancellationToken = default)
  {
    var queryable = context.Amenities
      .Where(query.Filter)
      .Sort(SortingExpressions.GetAmenitySortExpression(query.SortColumn), query.SortOrder);

    var itemsToReturn = await queryable
      .GetPage(query.PageNumber, query.PageSize)
      .AsNoTracking()
      .ToListAsync(cancellationToken);

    return new PaginatedList<Amenity>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }
}