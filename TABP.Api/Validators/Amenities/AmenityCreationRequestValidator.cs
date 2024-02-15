using FluentValidation;
using TABP.Api.Dtos.Amenities;
using TABP.Application.Extensions.Validation;

namespace TABP.Api.Validators.Amenities;

public class AmenityCreationRequestValidator : AbstractValidator<AmenityCreationRequest>
{
  public AmenityCreationRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .ValidName(3, 30);

    RuleFor(x => x.Description)
      .MaximumLength(100);
  }
}