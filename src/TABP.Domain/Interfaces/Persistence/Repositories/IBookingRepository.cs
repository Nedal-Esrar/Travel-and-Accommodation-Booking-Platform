using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IBookingRepository
{
  Task<bool> ExistsAsync(Expression<Func<Booking, bool>> predicate,
                         CancellationToken cancellationToken = default);
  
  Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<Booking?> GetByIdAsync(Guid id, Guid guestId, bool includeInvoice = false,
    CancellationToken cancellationToken = default);

  Task<PaginatedList<Booking>> GetAsync(Query<Booking> query, CancellationToken cancellationToken = default);

  Task<IEnumerable<Booking>> GetRecentBookingsInDifferentHotelsByGuestId(Guid guestId, int count,
    CancellationToken cancellationToken = default);
}