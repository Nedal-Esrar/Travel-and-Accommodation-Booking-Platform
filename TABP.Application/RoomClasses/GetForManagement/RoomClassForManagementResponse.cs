using TABP.Application.Amenities.Common;
using TABP.Application.Discounts.GetById;

namespace TABP.Application.RoomClasses.GetForManagement;

public record RoomClassForManagementResponse(
  Guid RoomClassId,
  string Name,
  string? Description,
  int AdultsCapacity,
  int ChildrenCapacity,
  decimal PricePerNight,
  IEnumerable<AmenityResponse> Amenities,
  DiscountResponse? ActiveDiscount,
  DateTime CreatedAtUtc,
  DateTime? ModifiedAtUtc);