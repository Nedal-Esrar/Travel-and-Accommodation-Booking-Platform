namespace TABP.Domain.Interfaces.Services;

public interface IPdfService
{
  Task<byte[]> GeneratePdfFromHtmlAsync(string html, CancellationToken cancellationToken = default);
}