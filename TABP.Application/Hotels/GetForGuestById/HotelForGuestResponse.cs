namespace TABP.Application.Hotels.GetForGuestById;

public class HotelForGuestResponse
{
  public Guid Id { get; init; }
  public string Name { get; init; }
  public double Longitude { get; init; }
  public double Latitude { get; init; }
  public string CityName { get; init; }
  public string CountryName { get; init; }
  public int StarRating { get; init; }
  public float ReviewsRating { get; init; }
  public string? BriefDescription { get; init; }
  public string? Description { get; init; }
  public string? ThumbnailUrl { get; init; }
  public IEnumerable<string> GalleryUrls { get; init; }
}