namespace TABP.Application.Hotels.GetRecentlyVisited;

public record RecentlyVisitedHotelResponse(
  Guid Id,
  Guid BookingId,
  string Name,
  string CityName,
  string CityCountry,
  int StarRating,
  double ReviewsRating,
  DateOnly CheckInDateUtc,
  DateOnly CheckOutDateUtc,
  decimal TotalPrice,
  string? ThumbnailUrl);