using MediatR;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.RoomClasses.GetByHotelIdForGuest;

public class GetRoomClassesByHotelIdForGuestQuery : IRequest<PaginatedList<RoomClassForGuestResponse>>
{
  public Guid HotelId { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
}