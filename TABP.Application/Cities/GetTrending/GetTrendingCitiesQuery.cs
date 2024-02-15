using MediatR;

namespace TABP.Application.Cities.GetTrending;

public record GetTrendingCitiesQuery(int Count) : IRequest<IEnumerable<TrendingCityResponse>>;