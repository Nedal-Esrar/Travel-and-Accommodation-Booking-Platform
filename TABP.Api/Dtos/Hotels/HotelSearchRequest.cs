using TABP.Api.Dtos.Common;
using TABP.Domain.Enums;

namespace TABP.Api.Dtos.Hotels;

/// <param name="SortColumn">Should be empty, 'id', 'name', 'starRating', 'price', or 'ReviewsRating'.</param>
public class HotelSearchRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }  
  public DateOnly CheckInDateUtc { get; init; }
  public DateOnly CheckOutDateUtc { get; init; }
  public int NumberOfAdults { get; init; }
  public int NumberOfChildren { get; init; }
  public int NumberOfRooms { get; init; }
  public decimal? MinPrice { get; init; }
  public decimal? MaxPrice { get; init; }
  public int? MinStarRating { get; init; }
  public IEnumerable<RoomType>? RoomTypes { get; init; }
  public IEnumerable<Guid>? Amenities { get; init; }
}