using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TABP.Domain.Interfaces.Services;
using TABP.Shared.OptionsValidation;

namespace TABP.Infrastructure.Services.Email;

public static class EmailConfiguration
{
  public static IServiceCollection AddEmailInfrastructure(this IServiceCollection services)
  {
    services.AddScoped<IValidator<EmailConfig>, EmailConfigValidator>();

    services.AddOptions<EmailConfig>()
      .BindConfiguration(nameof(EmailConfig))
      .ValidateFluentValidation()
      .ValidateOnStart();

    services.AddTransient<IEmailService, EmailService>();

    return services;
  }
}