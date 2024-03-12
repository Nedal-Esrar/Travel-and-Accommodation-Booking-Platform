using TABP.Domain.Entities;

namespace TABP.Domain.Models;

public class HotelSearchResult
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public int StarRating { get; set; }
  public double ReviewsRating { get; set; }
  public string? BriefDescription { get; set; }
  public Image? Thumbnail { get; set; }
  public decimal PricePerNightStartingAt { get; set; }
}