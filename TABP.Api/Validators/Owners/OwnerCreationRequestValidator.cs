using FluentValidation;
using TABP.Api.Dtos.Owners;
using TABP.Application.Extensions.Validation;
using static TABP.Domain.Constants.Common;

namespace TABP.Api.Validators.Owners;

public class OwnerCreationRequestValidator : AbstractValidator<OwnerCreationRequest>
{
  public OwnerCreationRequestValidator()
  {
    RuleFor(c => c.FirstName)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.LastName)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .PhoneNumber();

    RuleFor(x => x.Email)
      .NotEmpty()
      .EmailAddress();
  }
}