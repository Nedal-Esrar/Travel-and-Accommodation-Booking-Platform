namespace TABP.Application.Hotels.Search;

public record HotelSearchResultResponse(
  Guid Id,
  string Name,
  int StarRating,
  double ReviewsRating,
  string? BriefDescription,
  string? ThumbnailUrl,
  decimal PricePerNightStartingAt);