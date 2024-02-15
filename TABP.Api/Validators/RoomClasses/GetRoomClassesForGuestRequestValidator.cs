using FluentValidation;
using TABP.Api.Dtos.RoomClasses;
using TABP.Api.Validators.Common;

namespace TABP.Api.Validators.RoomClasses;

public class GetRoomClassesForGuestRequestValidator : AbstractValidator<GetRoomClassesForGuestRequest>
{
  public GetRoomClassesForGuestRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
  }
}