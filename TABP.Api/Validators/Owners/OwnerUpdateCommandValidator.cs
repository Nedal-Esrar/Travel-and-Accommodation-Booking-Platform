using FluentValidation;
using TABP.Api.Dtos.Owners;
using TABP.Application.Extensions.Validation;

namespace TABP.Api.Validators.Owners;

public class OwnerUpdateCommandValidator : AbstractValidator<OwnerUpdateRequest>
{
  public OwnerUpdateCommandValidator()
  {
    RuleFor(x => x.FirstName)
      .NotEmpty()
      .ValidName(3, 30);

    RuleFor(x => x.LastName)
      .NotEmpty()
      .ValidName(3, 30);

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .PhoneNumber();

    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();
  }
}