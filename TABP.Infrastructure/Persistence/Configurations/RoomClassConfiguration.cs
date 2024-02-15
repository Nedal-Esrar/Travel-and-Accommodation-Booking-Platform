using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TABP.Domain.Entities;
using TABP.Domain.Enums;

namespace TABP.Infrastructure.Persistence.Configurations;

public class RoomClassConfiguration : IEntityTypeConfiguration<RoomClass>
{
  public void Configure(EntityTypeBuilder<RoomClass> builder)
  {
    builder.HasKey(rc => rc.Id);

    builder.Property(rc => rc.RoomType)
      .HasConversion(new EnumToStringConverter<RoomType>());
    
    builder.Ignore(h => h.Gallery);

    builder.HasMany(rc => rc.Rooms)
      .WithOne(r => r.RoomClass)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(rc => rc.Amenities)
      .WithMany(a => a.RoomClasses);
    
    builder.Property(rc => rc.PricePerNight)
      .HasPrecision(18, 2);
    
    builder.HasIndex(rc => rc.RoomType);

    builder.HasIndex(rc => new { rc.AdultsCapacity, rc.ChildrenCapacity });

    builder.HasIndex(rc => rc.PricePerNight);
  }
}