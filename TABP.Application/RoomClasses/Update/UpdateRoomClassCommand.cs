using MediatR;

namespace TABP.Application.RoomClasses.Update;

public record UpdateRoomClassCommand(
  Guid RoomClassId,
  string Name,
  string? Description,
  int AdultsCapacity,
  int ChildrenCapacity,
  decimal PricePerNight) : IRequest;