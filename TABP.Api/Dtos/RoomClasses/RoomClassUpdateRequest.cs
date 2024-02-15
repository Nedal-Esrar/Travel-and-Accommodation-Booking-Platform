namespace TABP.Api.Dtos.RoomClasses;

public record RoomClassUpdateRequest(
  string Name,
  string? Description,
  int AdultsCapacity,
  int ChildrenCapacity,
  decimal PricePerNight);