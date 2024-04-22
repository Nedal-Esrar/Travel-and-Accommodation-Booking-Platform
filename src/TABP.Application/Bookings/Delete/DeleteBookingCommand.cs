using MediatR;

namespace TABP.Application.Bookings.Delete;

public class DeleteBookingCommand : IRequest
{
  public Guid BookingId { get; init; }
}