using TABP.Application.Amenities.Common;
using TABP.Application.Discounts.GetById;
using TABP.Domain.Enums;

namespace TABP.Application.RoomClasses.GetByHotelIdForGuest;

public record RoomClassForGuestResponse(
  Guid Id,
  string Name,
  int AdultsCapacity,
  int ChildrenCapacity,
  decimal PricePerNight,
  string? Description,
  RoomType RoomType,
  IEnumerable<AmenityResponse> Amenities,
  DiscountResponse? ActiveDiscount,
  IEnumerable<string> GalleryUrls);