using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
  public void Configure(EntityTypeBuilder<City> builder)
  {
    builder.HasKey(c => c.Id);

    builder.HasMany(c => c.Hotels)
      .WithOne(h => h.City)
      .IsRequired()
      .OnDelete(DeleteBehavior.Restrict);

    builder.Ignore(c => c.Thumbnail);
  }
}