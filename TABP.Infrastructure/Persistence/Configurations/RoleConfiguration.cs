using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Persistence.Configurations;

public class RoleConfiguration : IEntityTypeConfiguration<Role>
{
  public void Configure(EntityTypeBuilder<Role> builder)
  {
    builder.HasKey(r => r.Id);

    builder.HasMany(r => r.Users)
      .WithMany(u => u.Roles);
    
    builder.HasData([
      new Role
      {
        Id = new Guid("3d736b28-cf80-4dc1-8e49-453e3760f0be"),
        Name = "Guest"
      },
      new Role
      {
        Id = new Guid("d2e7d2bb-bc77-4a6d-a43a-763716c6df8b"),
        Name = "Admin"
      }
    ]);
  }
}