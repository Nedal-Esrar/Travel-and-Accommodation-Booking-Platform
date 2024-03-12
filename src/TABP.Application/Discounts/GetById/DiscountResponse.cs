namespace TABP.Application.Discounts.GetById;

public class DiscountResponse
{
  public Guid Id { get; init; }
  public Guid RoomClassId { get; init; }
  public float Percentage { get; init; }
  public DateTime StartDateUtc { get; init; }
  public DateTime EndDateUtc { get; init; }
  public DateTime CreatedAtUtc { get; init; }
}