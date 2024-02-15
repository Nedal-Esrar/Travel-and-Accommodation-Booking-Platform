namespace TABP.Api.Dtos.Cities;

public record CityCreationRequest(
  string Name,
  string Country,
  string PostOffice);