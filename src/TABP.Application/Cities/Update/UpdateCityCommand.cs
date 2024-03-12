using MediatR;

namespace TABP.Application.Cities.Update;

public class UpdateCityCommand : IRequest
{
  public Guid CityId { get; init; }
  public string Name { get; init; }
  public string Country { get; init; }
  public string PostOffice { get; init; }
}