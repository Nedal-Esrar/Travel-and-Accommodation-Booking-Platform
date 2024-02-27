using FluentValidation;
using TABP.Api.Dtos.Auth;
using TABP.Application.Extensions.Validation;
using static TABP.Domain.Constants.Common;

namespace TABP.Api.Validators.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
  public RegisterRequestValidator()
  {
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.LastName)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();

    RuleFor(x => x.Password)
      .NotEmpty()
      .StrongPassword();
  }
}