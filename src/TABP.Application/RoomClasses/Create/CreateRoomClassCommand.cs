using MediatR;

namespace TABP.Application.RoomClasses.Create;

public class CreateRoomClassCommand : IRequest<Guid>
{
  public Guid HotelId { get; init; }
  public string Name { get; init; }
  public string? Description { get; init; }
  public int AdultsCapacity { get; init; }
  public int ChildrenCapacity { get; init; }
  public decimal PricePerNight { get; init; }
  public IEnumerable<Guid> AmenitiesIds { get; init; }
}