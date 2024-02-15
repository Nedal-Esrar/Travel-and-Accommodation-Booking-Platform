using MediatR;

namespace TABP.Application.Rooms.Create;

public record CreateRoomCommand(
  Guid RoomClassId,
  string Number) : IRequest<Guid>;