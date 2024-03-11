using MediatR;

namespace TABP.Application.RoomClasses.Update;

public class UpdateRoomClassCommand : IRequest
{
  public Guid RoomClassId { get; init; }
  public string Name { get; init; }
  public string? Description { get; init; }
  public int AdultsCapacity { get; init; }
  public int ChildrenCapacity { get; init; }
  public decimal PricePerNight { get; init; }
}