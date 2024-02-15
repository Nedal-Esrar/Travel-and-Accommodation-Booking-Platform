using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IDiscountRepository
{
  Task<Discount?> GetByIdAsync(Guid roomClassId, Guid id, CancellationToken cancellationToken);

  Task<bool> ExistsInTimeIntervalAsync(DateTime startDate, DateTime endDate,
    CancellationToken cancellationToken = default);

  Task<Discount> CreateAsync(Discount discount, CancellationToken cancellationToken = default);

  Task<bool> ExistsByIdAsync(Guid roomClassId, Guid id, CancellationToken cancellationToken = default);
  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);

  Task<PaginatedList<Discount>> GetAsync(PaginationQuery<Discount> query,
    CancellationToken cancellationToken = default);
}