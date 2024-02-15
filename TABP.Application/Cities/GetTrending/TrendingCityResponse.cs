namespace TABP.Application.Cities.GetTrending;

public record TrendingCityResponse(
  Guid Id,
  string Name,
  string Country,
  string? ThumbnailUrl);