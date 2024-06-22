using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IRoomClassRepository
{
  Task<bool> ExistsAsync(Expression<Func<RoomClass, bool>> predicate,
                         CancellationToken cancellationToken = default);
  Task<RoomClass?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<RoomClass> CreateAsync(RoomClass roomClass, CancellationToken cancellationToken = default);

  Task UpdateAsync(RoomClass roomClass, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task<PaginatedList<RoomClass>> GetAsync(
    Query<RoomClass> query,
    bool includeGallery = false,
    CancellationToken cancellationToken = default);

  Task<IEnumerable<RoomClass>> GetFeaturedDealsInDifferentHotelsAsync(int count,
    CancellationToken cancellationToken = default);
}