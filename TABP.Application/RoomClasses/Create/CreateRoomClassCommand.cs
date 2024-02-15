using MediatR;

namespace TABP.Application.RoomClasses.Create;

public record CreateRoomClassCommand(
  Guid HotelId,
  string Name,
  string? Description,
  int AdultsCapacity,
  int ChildrenCapacity,
  decimal PricePerNight,
  IEnumerable<Guid> AmenitiesIds) : IRequest<Guid>;