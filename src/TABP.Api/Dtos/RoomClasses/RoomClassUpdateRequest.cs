namespace TABP.Api.Dtos.RoomClasses;

public class RoomClassUpdateRequest
{
  public string Name { get; init; }
  public string? Description { get; init; }
  public int AdultsCapacity { get; init; }
  public int ChildrenCapacity { get; init; }
  public decimal PricePerNight { get; init; }
}