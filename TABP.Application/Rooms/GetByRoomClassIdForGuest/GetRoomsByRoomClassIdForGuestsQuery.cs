using MediatR;
using TABP.Domain.Models;

namespace TABP.Application.Rooms.GetByRoomClassIdForGuest;

public class GetRoomsByRoomClassIdForGuestsQuery : IRequest<PaginatedList<RoomForGuestResponse>>
{
  public Guid RoomClassId { get; init; }
  public DateOnly CheckInDate { get; init; }
  public DateOnly CheckOutDate { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}