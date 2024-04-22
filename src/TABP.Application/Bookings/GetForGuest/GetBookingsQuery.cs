using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Bookings.GetForGuest;

public class GetBookingsQuery : IRequest<PaginatedList<BookingResponse>>
{
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
}