using MediatR;

namespace TABP.Application.RoomClasses.Delete;

public record DeleteRoomClassCommand(Guid RoomClassId) : IRequest;