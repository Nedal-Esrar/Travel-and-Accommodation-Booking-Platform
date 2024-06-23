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

public class ReviewRepository(HotelBookingDbContext context) : IReviewRepository
{
  public async Task<bool> ExistsAsync(Expression<Func<Review, bool>> predicate,
                                      CancellationToken cancellationToken = default)
  {
    return await context.Reviews.AnyAsync(predicate, cancellationToken);
  }
  public async Task<PaginatedList<Review>> GetAsync(Query<Review> query, 
    CancellationToken cancellationToken = default)
  {
    var queryable = context.Reviews
      .Where(query.Filter)
      .Sort(SortingExpressions.GetReviewSortExpression(query.SortColumn), query.SortOrder);

    var itemsToReturn = await queryable
      .GetPage(query.PageNumber, query.PageSize)
      .AsNoTracking()
      .ToListAsync(cancellationToken);

    return new PaginatedList<Review>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }

  public async Task<Review?> GetByIdAsync(Guid hotelId, 
    Guid reviewId, CancellationToken cancellationToken = default)
  {
    return await context.Reviews
      .FirstOrDefaultAsync(r => r.Id == reviewId && r.HotelId == hotelId, cancellationToken);
  }

  public async Task<Review> CreateAsync(Review review, 
    CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(review);

    var addedReview = await context.Reviews.AddAsync(review, cancellationToken);

    return addedReview.Entity;
  }

  public async Task<Review?> GetByIdAsync(Guid reviewId, Guid hotelId, 
    Guid guestId, CancellationToken cancellationToken = default)
  {
    return await context.Reviews
      .FirstOrDefaultAsync(r => r.Id == reviewId && r.HotelId == hotelId && r.GuestId == guestId, 
        cancellationToken);
  }

  public async Task DeleteAsync(Guid reviewId, 
    CancellationToken cancellationToken = default)
  {
    if (!await context.Reviews.AnyAsync(r => r.Id == reviewId, cancellationToken))
    {
      throw new NotFoundException(ReviewMessages.NotFound);
    }
    
    var entity = context.ChangeTracker.Entries<Review>()
      .FirstOrDefault(e => e.Entity.Id == reviewId)?.Entity
      ?? new Review { Id = reviewId };

    context.Reviews.Remove(entity);
  }

  public async Task<int> GetTotalRatingForHotelAsync(
    Guid hotelId, CancellationToken cancellationToken = default)
  {
    return await context.Reviews
      .Where(r => r.HotelId == hotelId)
      .SumAsync(r => r.Rating, cancellationToken);
  }

  public async Task<int> GetReviewCountForHotelAsync(
    Guid hotelId, CancellationToken cancellationToken = default)
  {
    return await context.Reviews
      .Where(r => r.HotelId == hotelId)
      .CountAsync(cancellationToken);
  }

  public async Task UpdateAsync(Review review, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(review);

    if (!await context.Reviews.AnyAsync(r => r.Id == review.Id, cancellationToken))
    {
      throw new NotFoundException(ReviewMessages.NotFound);
    }

    context.Reviews.Update(review);
  }
}