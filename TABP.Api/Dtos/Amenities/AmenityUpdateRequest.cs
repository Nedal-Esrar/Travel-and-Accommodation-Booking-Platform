namespace TABP.Api.Dtos.Amenities;

public record AmenityUpdateRequest(
  string Name,
  string? Description);