using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IOwnerRepository
{
  Task<bool> ExistsAsync(Expression<Func<Owner, bool>> predicate,
                         CancellationToken cancellationToken = default);
  
  Task<PaginatedList<Owner>> GetAsync(Query<Owner> query, CancellationToken cancellationToken = default);

  Task<Owner?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<Owner> CreateAsync(Owner owner, CancellationToken cancellationToken = default);

  Task UpdateAsync(Owner owner, CancellationToken cancellationToken = default);
}