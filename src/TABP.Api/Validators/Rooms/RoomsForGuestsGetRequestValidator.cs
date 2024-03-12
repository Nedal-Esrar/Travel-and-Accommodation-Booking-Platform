using FluentValidation;
using TABP.Api.Dtos.Rooms;
using TABP.Api.Validators.Common;

namespace TABP.Api.Validators.Rooms;

public class RoomsForGuestsGetRequestValidator : AbstractValidator<RoomsForGuestsGetRequest>
{
  public RoomsForGuestsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());

    RuleFor(x => x.CheckInDateUtc)
      .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

    RuleFor(x => x.CheckOutDateUtc)
      .GreaterThanOrEqualTo(x => x.CheckInDateUtc);
  }
}