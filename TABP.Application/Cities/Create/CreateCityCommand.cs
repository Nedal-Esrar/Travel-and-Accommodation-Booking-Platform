using MediatR;

namespace TABP.Application.Cities.Create;

public class CreateCityCommand : IRequest<CityResponse>
{
  public string Name { get; init; }
  public string Country { get; init; }
  public string PostOffice { get; init; }
}