using MediatR;

namespace TABP.Application.Bookings.Delete;

public record DeleteBookingCommand(
  Guid GuestId,
  Guid BookingId) : IRequest;