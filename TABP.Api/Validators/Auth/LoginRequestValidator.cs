using FluentValidation;
using TABP.Api.Dtos.Auth;
using TABP.Application.Extensions.Validation;

namespace TABP.Api.Validators.Auth;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
  public LoginRequestValidator()
  {
    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();

    RuleFor(x => x.Password)
      .NotEmpty()
      .StrongPassword();
  }
}