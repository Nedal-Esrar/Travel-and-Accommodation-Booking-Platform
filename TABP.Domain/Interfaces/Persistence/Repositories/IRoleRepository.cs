using TABP.Domain.Entities;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IRoleRepository
{
  Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}