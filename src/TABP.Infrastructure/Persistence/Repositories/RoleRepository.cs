using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Infrastructure.Persistence.DbContexts;

namespace TABP.Infrastructure.Persistence.Repositories;

public class RoleRepository(HotelBookingDbContext context) : IRoleRepository
{
  public async Task<Role?> GetByNameAsync(string name, CancellationToken cancellationToken = default)
  {
    return await context.Roles
      .FirstOrDefaultAsync(r => r.Name == name, cancellationToken);
  }
}