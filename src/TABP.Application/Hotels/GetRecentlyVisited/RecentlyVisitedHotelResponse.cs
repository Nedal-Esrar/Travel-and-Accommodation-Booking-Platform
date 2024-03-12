namespace TABP.Application.Hotels.GetRecentlyVisited;

public class RecentlyVisitedHotelResponse
{
  public Guid Id { get; init; }
  public Guid BookingId { get; init; }
  public string Name { get; init; }
  public string CityName { get; init; }
  public string CityCountry { get; init; }
  public int StarRating { get; init; }
  public double ReviewsRating { get; init; }
  public DateOnly CheckInDateUtc { get; init; }
  public DateOnly CheckOutDateUtc { get; init; }
  public decimal TotalPrice { get; init; }
  public string? ThumbnailUrl { get; init; }
}