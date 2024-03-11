using MediatR;

namespace TABP.Application.RoomClasses.Delete;

public class DeleteRoomClassCommand : IRequest
{
  public Guid RoomClassId { get; init; }
}