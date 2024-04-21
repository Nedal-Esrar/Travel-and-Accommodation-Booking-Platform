using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TABP.Shared.OptionsValidation;

public static class OptionsBuilderExtensions
{
  public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(
    this OptionsBuilder<TOptions> builder)
    where TOptions : class
  {
    builder.Services.AddSingleton<IValidateOptions<TOptions>, FluentValidateOptions<TOptions>>();

    return builder;
  }
}