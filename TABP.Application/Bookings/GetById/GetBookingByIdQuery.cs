using MediatR;
using TABP.Application.Bookings.Common;

namespace TABP.Application.Bookings.GetById;

public record GetBookingByIdQuery(
  Guid GuestId,
  Guid BookingId) : IRequest<BookingResponse>;