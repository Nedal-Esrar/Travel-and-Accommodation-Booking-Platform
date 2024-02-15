using MediatR;
using TABP.Domain.Models;

namespace TABP.Application.Rooms.GetByRoomClassIdForGuest;

public record GetRoomsByRoomClassIdForGuestsQuery(
  Guid RoomClassId,
  DateOnly CheckInDate,
  DateOnly CheckOutDate,
  int PageNumber,
  int PageSize) : IRequest<PaginatedList<RoomForGuestResponse>>;