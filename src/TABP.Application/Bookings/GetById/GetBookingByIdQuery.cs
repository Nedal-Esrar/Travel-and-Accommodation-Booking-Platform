using MediatR;
using TABP.Application.Bookings.Common;

namespace TABP.Application.Bookings.GetById;

public class GetBookingByIdQuery : IRequest<BookingResponse>
{
  public Guid BookingId { get; init; }
}