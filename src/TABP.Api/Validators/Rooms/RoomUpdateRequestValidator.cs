using FluentValidation;
using TABP.Api.Dtos.Rooms;

namespace TABP.Api.Validators.Rooms;

public class RoomUpdateRequestValidator : AbstractValidator<RoomUpdateRequest>
{
  public RoomUpdateRequestValidator()
  {
    RuleFor(x => x.Number)
      .NotEmpty();
  }
}