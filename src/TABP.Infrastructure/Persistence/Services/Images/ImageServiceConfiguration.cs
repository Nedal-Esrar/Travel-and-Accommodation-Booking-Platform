using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TABP.Domain.Interfaces.Persistence.Services;
using TABP.Shared.OptionsValidation;

namespace TABP.Infrastructure.Persistence.Services.Images;

public static class ImageServiceConfiguration
{
  public static IServiceCollection AddImageService(this IServiceCollection services)
  {
    services.AddScoped<IValidator<FirebaseConfig>, FireBaseConfigValidator>();

    services.AddOptions<FirebaseConfig>()
      .BindConfiguration(nameof(FirebaseConfig))
      .ValidateFluentValidation()
      .ValidateOnStart();
    
    services.AddScoped<IImageService, FirebaseImageService>();

    return services;
  }
}