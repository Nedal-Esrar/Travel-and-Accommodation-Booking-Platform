namespace TABP.Api.Dtos.Cities;

public record CityUpdateRequest(
  string Name,
  string Country,
  string PostOffice);