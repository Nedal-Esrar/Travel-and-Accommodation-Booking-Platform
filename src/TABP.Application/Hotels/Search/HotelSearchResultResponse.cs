namespace TABP.Application.Hotels.Search;

public class HotelSearchResultResponse
{
  public Guid Id { get; init; }
  public string Name { get; init; }
  public int StarRating { get; init; }
  public double ReviewsRating { get; init; }
  public string? BriefDescription { get; init; }
  public string? ThumbnailUrl { get; init; }
  public decimal PricePerNightStartingAt { get; init; }
}