namespace TABP.Application.Cities.Create;

public record CityResponse(
  Guid Id,
  string Name,
  string Country,
  string PostOffice);