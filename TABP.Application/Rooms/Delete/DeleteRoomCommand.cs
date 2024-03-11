using MediatR;

namespace TABP.Application.Rooms.Delete;

public class DeleteRoomCommand : IRequest
{
  public Guid RoomClassId { get; init; } 
  public Guid RoomId { get; init; }
}