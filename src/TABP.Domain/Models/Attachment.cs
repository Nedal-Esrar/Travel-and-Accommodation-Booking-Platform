namespace TABP.Domain.Models;

public record Attachment(
  string Name,
  byte[] File,
  string MediaType,
  string SubMediaType);