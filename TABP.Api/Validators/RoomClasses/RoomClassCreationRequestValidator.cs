using FluentValidation;
using TABP.Api.Dtos.RoomClasses;
using TABP.Application.Extensions.Validation;

namespace TABP.Api.Validators.RoomClasses;

public class RoomClassCreationRequestValidator : AbstractValidator<RoomClassCreationRequest>
{
  public RoomClassCreationRequestValidator()
  {
    RuleFor(x => x.HotelId)
      .NotEmpty();

    RuleFor(x => x.Name)
      .NotEmpty()
      .ValidName(3, 30);

    RuleFor(x => x.Description)
      .MaximumLength(100);

    RuleFor(x => x.AdultCapacity)
      .NotEmpty()
      .InclusiveBetween(1, 5);

    RuleFor(x => x.ChildrenCapacity)
      .NotEmpty()
      .InclusiveBetween(0, 5);

    RuleFor(x => x.PricePerNight)
      .NotEmpty()
      .GreaterThan(0);
  }
}