using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IAmenityRepository
{
  Task<PaginatedList<Amenity>> GetAsync(Query<Amenity> query, CancellationToken cancellationToken = default);

  Task<Amenity?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<bool> ExistsAsync(Expression<Func<Amenity, bool>> predicate, CancellationToken cancellationToken = default);

  Task<Amenity> CreateAsync(Amenity amenity, CancellationToken cancellationToken = default);

  Task UpdateAsync(Amenity amenity, CancellationToken cancellationToken = default);
}