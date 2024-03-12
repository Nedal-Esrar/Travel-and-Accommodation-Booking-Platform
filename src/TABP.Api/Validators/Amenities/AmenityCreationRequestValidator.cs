using FluentValidation;
using TABP.Api.Dtos.Amenities;
using TABP.Application.Extensions.Validation;
using static TABP.Domain.Constants.Common;

namespace TABP.Api.Validators.Amenities;

public class AmenityCreationRequestValidator : AbstractValidator<AmenityCreationRequest>
{
  public AmenityCreationRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.Description)
      .MaximumLength(ShortTextMaxLength);
  }
}