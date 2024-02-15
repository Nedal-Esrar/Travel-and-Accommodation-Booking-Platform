using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace TABP.Application;

public static class ApplicationConfiguration
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

    services.AddAutoMapper(Assembly.GetExecutingAssembly());

    return services;
  }
}