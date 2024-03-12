namespace TABP.Domain.Models;

public class RoomForManagement
{
  public Guid Id { get; set; }
  public Guid RoomClassId { get; set; }
  public string Number { get; set; }
  public bool IsAvailable { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
}