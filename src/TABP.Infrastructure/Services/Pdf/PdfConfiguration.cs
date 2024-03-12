using Microsoft.Extensions.DependencyInjection;
using TABP.Domain.Interfaces.Services;

namespace TABP.Infrastructure.Services.Pdf;

public static class PdfConfiguration
{
  public static IServiceCollection AddPdfInfrastructure(this IServiceCollection services)
  {
    services.AddScoped<IPdfService, PdfService>();

    return services;
  }
}