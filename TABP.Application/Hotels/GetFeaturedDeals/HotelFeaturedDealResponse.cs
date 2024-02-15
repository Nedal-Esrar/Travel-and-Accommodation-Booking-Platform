namespace TABP.Application.Hotels.GetFeaturedDeals;

public record HotelFeaturedDealResponse(
  Guid Id,
  string Name,
  string RoomClassName,
  string? Description,
  int StarRating,
  double Longitude,
  double Latitude,
  string CityName,
  string CountryName,
  decimal OriginalPricePerNight,
  decimal DiscountPercentage,
  DateTime DiscountStartDateUtc,
  DateTime DiscountEndDateUtc,
  string? ThumbnailUrl);