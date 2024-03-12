namespace TABP.Domain.Entities;

public class Discount : EntityBase
{
  public Guid RoomClassId { get; set; }
  public RoomClass RoomClass { get; set; }
  public decimal Percentage { get; set; }
  public DateTime StartDateUtc { get; set; }
  public DateTime EndDateUtc { get; set; }
  public DateTime CreatedAtUtc { get; set; }
}