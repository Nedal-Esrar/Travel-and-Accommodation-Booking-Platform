using MediatR;

namespace TABP.Application.Rooms.Delete;

public record DeleteRoomCommand(Guid RoomClassId, Guid RoomId) : IRequest;