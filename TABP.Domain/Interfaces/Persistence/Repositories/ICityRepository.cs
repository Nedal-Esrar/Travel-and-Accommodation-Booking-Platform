using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface ICityRepository
{
  Task<PaginatedList<CityForManagement>> GetForManagementAsync(PaginationQuery<City> query,
    CancellationToken cancellationToken = default);

  Task<City?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<City> CreateAsync(City city, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task UpdateAsync(City city, CancellationToken cancellationToken = default);

  Task<bool> ExistsByPostOfficeAsync(string postOffice, CancellationToken cancellationToken = default);

  Task<IEnumerable<City>> GetMostVisitedAsync(int count, CancellationToken cancellationToken = default);
}