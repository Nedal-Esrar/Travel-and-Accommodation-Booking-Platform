namespace TABP.Application.Hotels.GetFeaturedDeals;

public class HotelFeaturedDealResponse
{
  public Guid Id { get; init; }
  public string Name { get; init; }
  public string RoomClassName { get; init; }
  public string? Description { get; init; }
  public int StarRating { get; init; }
  public double Longitude { get; init; }
  public double Latitude { get; init; }
  public string CityName { get; init; }
  public string CountryName { get; init; }
  public decimal OriginalPricePerNight { get; init; }
  public decimal DiscountPercentage { get; init; }
  public DateTime DiscountStartDateUtc { get; init; }
  public DateTime DiscountEndDateUtc { get; init; }
  public string? ThumbnailUrl { get; init; }
}