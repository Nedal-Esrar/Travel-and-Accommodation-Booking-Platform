using System.Numerics;
using FluentValidation;
using TABP.Api.Dtos.RoomClasses;
using TABP.Application.Extensions.Validation;
using static TABP.Domain.Constants.Common;
using static TABP.Domain.Constants.RoomClass;

namespace TABP.Api.Validators.RoomClasses;

public class RoomClassCreationRequestValidator : AbstractValidator<RoomClassCreationRequest>
{
  public RoomClassCreationRequestValidator()
  {
    RuleFor(x => x.HotelId)
      .NotEmpty();

    RuleFor(x => x.Name)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.Description)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.AdultsCapacity)
      .NotEmpty()
      .InclusiveBetween(MinAdultsCapacity, MaxAdultsCapacity);

    RuleFor(x => x.ChildrenCapacity)
      .NotNull()
      .InclusiveBetween(MinChildrenCapacity, MaxChildrenCapacity);

    RuleFor(x => x.PricePerNight)
      .NotEmpty()
      .GreaterThan(Zero);
  }
}