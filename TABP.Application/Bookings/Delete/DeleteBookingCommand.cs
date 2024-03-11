using MediatR;

namespace TABP.Application.Bookings.Delete;

public class DeleteBookingCommand : IRequest
{
  public Guid GuestId { get; init; }
  public Guid BookingId { get; init; }
}