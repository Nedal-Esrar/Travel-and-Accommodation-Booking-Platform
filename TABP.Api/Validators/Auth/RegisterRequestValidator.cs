using FluentValidation;
using TABP.Api.Dtos.Auth;
using TABP.Application.Extensions.Validation;

namespace TABP.Api.Validators.Auth;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
  public RegisterRequestValidator()
  {
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .ValidName(3, 30);

    RuleFor(x => x.LastName)
      .NotEmpty()
      .ValidName(3, 30);

    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();

    RuleFor(x => x.Password)
      .NotEmpty()
      .StrongPassword();
  }
}