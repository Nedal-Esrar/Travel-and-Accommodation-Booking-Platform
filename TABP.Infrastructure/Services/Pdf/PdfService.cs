using NReco.PdfGenerator;
using TABP.Domain.Interfaces.Services;

namespace TABP.Infrastructure.Services.Pdf;

public class PdfService : IPdfService
{
  public async Task<byte[]> GeneratePdfFromHtmlAsync(string html, CancellationToken cancellationToken = default)
  {
    return await Task.Run(() =>
    {
      var htmlToPdfConverter = new HtmlToPdfConverter();

      return htmlToPdfConverter.GeneratePdf(html);
    }, cancellationToken);
  }
}