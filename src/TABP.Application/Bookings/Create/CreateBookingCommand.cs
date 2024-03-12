using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Domain.Enums;

namespace TABP.Application.Bookings.Create;

public class CreateBookingCommand : IRequest<BookingResponse>
{
  public Guid GuestId { get; init; }
  public IEnumerable<Guid> RoomIds { get; init; }
  public Guid HotelId { get; init; }
  public DateOnly CheckInDateUtc { get; init; }
  public DateOnly CheckOutDateUtc { get; init; }
  public string? GuestRemarks { get; init; }
  public PaymentMethod PaymentMethod { get; init; }
}