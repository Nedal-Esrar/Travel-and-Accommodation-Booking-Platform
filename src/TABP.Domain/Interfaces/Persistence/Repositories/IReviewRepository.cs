using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IReviewRepository
{
  Task<bool> ExistsAsync(Expression<Func<Review, bool>> predicate,
                         CancellationToken cancellationToken = default); 
  public Task<PaginatedList<Review>> GetAsync(Query<Review> query,
    CancellationToken cancellationToken = default);

  Task<Review?> GetByIdAsync(Guid hotelId, Guid reviewId, CancellationToken cancellationToken = default);
  
  Task<Review> CreateAsync(Review review, CancellationToken cancellationToken = default);
  
  Task<Review?> GetByIdAsync(Guid reviewId, Guid hotelId, Guid guestId, CancellationToken cancellationToken = default);
 
  Task DeleteAsync(Guid reviewId, CancellationToken cancellationToken = default);
  
  Task<int> GetTotalRatingForHotelAsync(Guid hotelId, CancellationToken cancellationToken = default);
  
  Task<int> GetReviewCountForHotelAsync(Guid hotelId, CancellationToken cancellationToken = default);
  
  Task UpdateAsync(Review review, CancellationToken cancellationToken = default);
}