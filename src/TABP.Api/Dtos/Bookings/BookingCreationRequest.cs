using TABP.Domain.Enums;

namespace TABP.Api.Dtos.Bookings;

public class BookingCreationRequest
{
  public IEnumerable<Guid> RoomIds { get; init; }
  public Guid HotelId { get; init; }
  public DateOnly CheckInDateUtc { get; init; }
  public DateOnly CheckOutDateUtc { get; init; }
  public string? GuestRemarks { get; init; }
  public PaymentMethod PaymentMethod { get; init; }
}