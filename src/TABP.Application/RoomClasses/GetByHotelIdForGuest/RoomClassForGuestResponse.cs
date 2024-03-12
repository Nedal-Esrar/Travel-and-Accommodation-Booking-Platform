using TABP.Application.Amenities.Common;
using TABP.Application.Discounts.GetById;
using TABP.Domain.Enums;

namespace TABP.Application.RoomClasses.GetByHotelIdForGuest;

public class RoomClassForGuestResponse
{
  public Guid Id { get; init; }
  public string Name { get; init; }
  public int AdultsCapacity { get; init; }
  public int ChildrenCapacity { get; init; }
  public decimal PricePerNight { get; init; }
  public string? Description { get; init; }
  public RoomType RoomType { get; init; }
  public IEnumerable<AmenityResponse> Amenities { get; init; }
  public DiscountResponse? ActiveDiscount { get; init; }
  public IEnumerable<string> GalleryUrls { get; init; }
}