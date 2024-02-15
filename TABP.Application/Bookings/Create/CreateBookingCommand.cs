using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Domain.Enums;

namespace TABP.Application.Bookings.Create;

public record CreateBookingCommand(
  Guid GuestId,
  IEnumerable<Guid> RoomIds,
  Guid HotelId,
  DateOnly CheckInDateUtc,
  DateOnly CheckOutDateUtc,
  string? GuestRemarks,
  PaymentMethod PaymentMethod) : IRequest<BookingResponse>;