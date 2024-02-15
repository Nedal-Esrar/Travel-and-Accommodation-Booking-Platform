using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TABP.Domain.Entities;
using TABP.Domain.Enums;

namespace TABP.Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
  public void Configure(EntityTypeBuilder<Image> builder)
  {
    builder.HasKey(i => i.Id);

    builder.Property(i => i.Type)
      .HasConversion(new EnumToStringConverter<ImageType>());
    
    builder.HasIndex(i => i.EntityId);
  }
}