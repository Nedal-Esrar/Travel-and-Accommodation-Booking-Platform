using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);

    builder.HasIndex(u => u.Email).IsUnique();
    
    builder.HasData([
      new User
      {
        Id = new Guid("ba867dc2-8104-4765-836d-bce1a98ebb03"),
        FirstName = "Admin",
        LastName = "Admin",
        Email = "admin@hotelbooking.com",
        Password = "ALaU/k/AAn4cw05diU6Bb0Va6ZCQt2dDewGrngK3ez2i4TBtRCSEzIgStvbVeRZB8A==" // hashed password.
      }
    ]);
    
    builder.HasMany(u => u.Roles)
      .WithMany(r => r.Users)
      .UsingEntity<Dictionary<string, object>>(
        "UserRole",
        j => j.HasOne<Role>().WithMany()
          .HasForeignKey("RoleId").OnDelete(DeleteBehavior.Cascade),
        j => j.HasOne<User>().WithMany()
          .HasForeignKey("UserId").OnDelete(DeleteBehavior.Cascade))
      .HasData([new Dictionary<string, object>{
        ["UserId"] = new Guid("ba867dc2-8104-4765-836d-bce1a98ebb03"), 
        ["RoleId"] = new Guid("d2e7d2bb-bc77-4a6d-a43a-763716c6df8b")
      }]);
  }
}