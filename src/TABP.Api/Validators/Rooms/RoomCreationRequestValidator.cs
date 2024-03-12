using FluentValidation;
using TABP.Api.Dtos.Rooms;

namespace TABP.Api.Validators.Rooms;

public class RoomCreationRequestValidator : AbstractValidator<RoomCreationRequest>
{
  public RoomCreationRequestValidator()
  {
    RuleFor(x => x.Number)
      .NotEmpty();
  }
}