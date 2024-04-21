using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TABP.Domain.Interfaces.Services;
using TABP.Infrastructure.Auth.Jwt;
using TABP.Infrastructure.Persistence;
using TABP.Infrastructure.Services.Date;
using TABP.Infrastructure.Services.Email;
using TABP.Infrastructure.Services.Pdf;

namespace TABP.Infrastructure;

public static class DependencyInjection
{
  public static IServiceCollection AddInfrastructure(
    this IServiceCollection services, 
    IConfiguration config)
  {
    services.AddPersistenceInfrastructure(config)
      .AddAuthInfrastructure()
      .AddEmailInfrastructure()
      .AddPdfInfrastructure()
      .AddTransient<IDateTimeProvider, DateTimeProvider>();

    return services;
  }
}