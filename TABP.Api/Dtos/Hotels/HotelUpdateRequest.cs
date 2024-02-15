namespace TABP.Api.Dtos.Hotels;

public record HotelUpdateRequest(
  Guid CityId,
  Guid OwnerId,
  string Name,
  int StarRating,
  double Longitude,
  double Latitude,
  string? BriefDescription,
  string? Description,
  string PhoneNumber);