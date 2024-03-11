namespace TABP.Application.Rooms.GetForManagement;

public class RoomForManagementResponse
{
  public Guid Id { get; init; }
  public Guid RoomClassId { get; init; }
  public string Number { get; init; }
  public DateTime CreatedAtUtc { get; init; }
  public DateTime? ModifiedAtUtc { get; init; }
  public bool IsAvailable { get; init; }
}