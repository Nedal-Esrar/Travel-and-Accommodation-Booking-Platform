using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Domain.Models;

namespace TABP.Application.Bookings.GetForGuest;

public record GetBookingsQuery(
  Guid GuestId,
  int PageNumber,
  int PageSize) : IRequest<PaginatedList<BookingResponse>>;