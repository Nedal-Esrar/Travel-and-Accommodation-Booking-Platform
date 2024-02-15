using MediatR;

namespace TABP.Application.Cities.Delete;

public record DeleteCityCommand(Guid CityId) : IRequest;