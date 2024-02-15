using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Persistence.Configurations;

public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
{
  public void Configure(EntityTypeBuilder<Hotel> builder)
  {
    builder.HasKey(h => h.Id);

    builder.HasMany(h => h.RoomClasses)
      .WithOne(rc => rc.Hotel)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);

    builder.HasMany(h => h.Bookings)
      .WithOne(b => b.Hotel)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);

    builder.Ignore(h => h.Gallery);
    
    builder.Ignore(h => h.Thumbnail);
    
    builder.Property(h => h.ReviewsRating)
      .HasPrecision(8, 6);

    builder.Property(h => h.Longitude)
      .HasPrecision(8, 6);

    builder.Property(h => h.Latitude)
      .HasPrecision(8, 6);
    
    builder.HasIndex(h => h.StarRating);
  }
}