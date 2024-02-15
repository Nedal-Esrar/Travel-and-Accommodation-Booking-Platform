using FluentValidation;
using TABP.Api.Dtos.RoomClasses;
using TABP.Application.Extensions.Validation;

namespace TABP.Api.Validators.RoomClasses;

public class RoomClassUpdateRequestValidator : AbstractValidator<RoomClassUpdateRequest>
{
  public RoomClassUpdateRequestValidator()
  {
    RuleFor(x => x.Name)
      .NotEmpty()
      .ValidName(3, 30);

    RuleFor(x => x.Description)
      .MaximumLength(100);

    RuleFor(x => x.AdultsCapacity)
      .NotEmpty()
      .GreaterThan(0);

    RuleFor(x => x.ChildrenCapacity)
      .NotEmpty()
      .GreaterThanOrEqualTo(0);

    RuleFor(x => x.PricePerNight)
      .NotEmpty()
      .GreaterThan(0);
  }
}