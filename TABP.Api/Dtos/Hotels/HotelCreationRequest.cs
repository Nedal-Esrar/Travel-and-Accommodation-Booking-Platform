namespace TABP.Api.Dtos.Hotels;

public record HotelCreationRequest(
  Guid CityId,
  Guid OwnerId,
  string Name,
  int StarRating,
  double Longitude,
  double Latitude,
  string? BriefDescription,
  string? Description,
  string PhoneNumber);