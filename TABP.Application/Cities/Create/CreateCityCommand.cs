using MediatR;

namespace TABP.Application.Cities.Create;

public record CreateCityCommand(
  string Name,
  string Country,
  string PostOffice) : IRequest<CityResponse>;