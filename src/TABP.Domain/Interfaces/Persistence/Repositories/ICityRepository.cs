using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface ICityRepository
{
  Task<bool> ExistsAsync(Expression<Func<City, bool>> predicate,
                         CancellationToken cancellationToken = default);
  Task<PaginatedList<CityForManagement>> GetForManagementAsync(Query<City> query,
    CancellationToken cancellationToken = default);

  Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<City> CreateAsync(City city, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task UpdateAsync(City city, CancellationToken cancellationToken = default);

  Task<IEnumerable<City>> GetMostVisitedAsync(int count, CancellationToken cancellationToken = default);
}