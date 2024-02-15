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

public class DiscountRepository(HotelBookingDbContext context) : IDiscountRepository
{
  public Task<Discount?> GetByIdAsync(
    Guid roomClassId, Guid id,
    CancellationToken cancellationToken = default)
  {
    return context.Discounts
      .SingleOrDefaultAsync(d => d.Id == id && d.RoomClassId == roomClassId,
        cancellationToken);
  }

  public async Task<bool> ExistsInTimeIntervalAsync(
    DateTime startDate, DateTime endDate,
    CancellationToken cancellationToken = default)
  {
    return await context.Discounts.AnyAsync(
      d => endDate >= d.StartDateUtc && startDate <= d.EndDateUtc,
      cancellationToken);
  }

  public async Task<Discount> CreateAsync(
    Discount discount,
    CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(discount);
    
    var createdDiscount = await context.Discounts.AddAsync(discount, cancellationToken);

    return createdDiscount.Entity;
  }

  public async Task<bool> ExistsByIdAsync(Guid roomClassId, Guid id, CancellationToken cancellationToken = default)
  {
    return await context.Discounts.AnyAsync(
      d => d.Id == id && d.RoomClassId == roomClassId, cancellationToken);
  }

  public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
  {
    if (!await context.Discounts.AnyAsync(r => r.Id == id, cancellationToken))
    {
      throw new NotFoundException(DiscountMessages.NotFound);
    }
    
    var entity = context.ChangeTracker.Entries<Discount>()
                   .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                 ?? new Discount { Id = id };

    context.Discounts.Remove(entity);
  }

  public async Task<PaginatedList<Discount>> GetAsync(
    PaginationQuery<Discount> query,
    CancellationToken cancellationToken = default)
  {
    var queryable = context.Discounts
      .Where(query.Filter)
      .Sort(SortingExpressions.GetDiscountSortExpression(query.SortColumn), query.SortOrder);

    var itemsToReturn = await queryable
      .GetPage(query.PageNumber, query.PageSize)
      .AsNoTracking()
      .ToListAsync(cancellationToken);

    return new PaginatedList<Discount>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }
}