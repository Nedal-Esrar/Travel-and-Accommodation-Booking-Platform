using System.Data;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Persistence;

namespace TABP.Infrastructure.Persistence.DbContexts;

public class UnitOfWork(HotelBookingDbContext context) : IUnitOfWork
{
  public async Task BeginTransactionAsync(CancellationToken cancellationToken = default)
  {
    await context.Database.BeginTransactionAsync(IsolationLevel.Serializable, cancellationToken);
  }

  public async Task CommitTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (context.Database.CurrentTransaction is null) return;
      
    await context.Database.CommitTransactionAsync(cancellationToken);
  }

  public async Task RollbackTransactionAsync(CancellationToken cancellationToken = default)
  {
    if (context.Database.CurrentTransaction is null) return;
    
    await context.Database.RollbackTransactionAsync(cancellationToken);
  }

  public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
  {
    context.ChangeTracker.DetectChanges();

    foreach (var entry in context.ChangeTracker.Entries<IAuditableEntity>())
      switch (entry.State)
      {
        case EntityState.Added:
          entry.Entity.CreatedAtUtc = DateTime.UtcNow;
          break;
        case EntityState.Modified:
          entry.Entity.ModifiedAtUtc = DateTime.UtcNow;
          break;
      }

    return await context.SaveChangesAsync(cancellationToken);
  }
}