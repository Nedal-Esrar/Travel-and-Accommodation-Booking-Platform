namespace TABP.Api.Dtos.RoomClasses;

public record RoomClassCreationRequest(
  Guid HotelId,
  string Name,
  string? Description,
  int AdultsCapacity,
  int ChildrenCapacity,
  decimal PricePerNight,
  IEnumerable<Guid>? AmenitiesIds);