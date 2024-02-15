using MediatR;
using TABP.Domain.Models;

namespace TABP.Application.RoomClasses.GetByHotelIdForGuest;

public record GetRoomClassesByHotelIdForGuestQuery(
  Guid HotelId,
  int PageNumber,
  int PageSize) : IRequest<PaginatedList<RoomClassForGuestResponse>>;