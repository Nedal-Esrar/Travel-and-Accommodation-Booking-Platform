using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Persistence.Configurations;

public class AmenityConfiguration : IEntityTypeConfiguration<Amenity>
{
  public void Configure(EntityTypeBuilder<Amenity> builder)
  {
    builder.HasKey(a => a.Id);

    builder.HasMany(a => a.RoomClasses)
      .WithMany(rc => rc.Amenities);
  }
}