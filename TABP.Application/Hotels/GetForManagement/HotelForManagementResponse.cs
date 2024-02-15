using TABP.Application.Owners.Common;

namespace TABP.Application.Hotels.GetForManagement;

public record HotelForManagementResponse(
  Guid Id,
  string Name,
  int StarRating,
  OwnerResponse Owner,
  int NumberOfRooms,
  DateTime CreatedAtUtc,
  DateTime? ModifiedAtUtc);