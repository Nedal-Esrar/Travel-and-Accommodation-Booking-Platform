using MediatR;

namespace TABP.Application.Cities.Delete;

public class DeleteCityCommand : IRequest
{
  public Guid CityId { get; init; }
}