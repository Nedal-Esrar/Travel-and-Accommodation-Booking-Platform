namespace TABP.Application.Rooms.GetForManagement;

public record RoomForManagementResponse(
  Guid Id,
  Guid RoomClassId,
  string Number,
  DateTime CreatedAtUtc,
  DateTime? ModifiedAtUtc,
  bool IsAvailable);