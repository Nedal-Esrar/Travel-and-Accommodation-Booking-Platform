namespace TABP.Api.Dtos.RoomClasses;

public record RoomClassCreationRequest(
  Guid HotelId,
  string Name,
  string? Description,
  int AdultCapacity,
  int ChildrenCapacity,
  decimal PricePerNight,
  IEnumerable<Guid>? AmenitiesIds);