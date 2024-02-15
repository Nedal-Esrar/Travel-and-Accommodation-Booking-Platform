using MediatR;

namespace TABP.Application.Rooms.Update;

public record UpdateRoomCommand(
  Guid RoomClassId,
  Guid RoomId,
  string Number) : IRequest;