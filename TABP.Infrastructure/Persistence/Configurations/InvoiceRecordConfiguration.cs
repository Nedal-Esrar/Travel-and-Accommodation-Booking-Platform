using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Persistence.Configurations;

public class InvoiceRecordConfiguration : IEntityTypeConfiguration<InvoiceRecord>
{
  public void Configure(EntityTypeBuilder<InvoiceRecord> builder)
  {
    builder.HasKey(ir => ir.Id);
  }
}