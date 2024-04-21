using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace TABP.Shared.OptionsValidation;

public class FluentValidateOptions<TOptions> : IValidateOptions<TOptions>
  where TOptions : class
{
  private readonly IServiceProvider _serviceProvider;

  public FluentValidateOptions(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public ValidateOptionsResult Validate(string? name, TOptions options)
  {
    ArgumentNullException.ThrowIfNull(options);

    using var scope = _serviceProvider.CreateScope();

    var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();

    var results = validator.Validate(options);

    if (results.IsValid)
    {
      return ValidateOptionsResult.Success;
    }

    var optionsTypeName = typeof(TOptions).Name;

    var errors = results.Errors.Select(e
      => ValidationMessages.GenerateValidationFailureMessages(optionsTypeName, e.PropertyName, e.ErrorMessage));

    return ValidateOptionsResult.Fail(errors);
  }
}