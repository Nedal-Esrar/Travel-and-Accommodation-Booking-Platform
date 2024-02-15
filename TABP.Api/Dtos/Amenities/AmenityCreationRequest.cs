namespace TABP.Api.Dtos.Amenities;

public record AmenityCreationRequest(
  string Name,
  string? Description);