namespace TABP.Application.Bookings.Common;

public class BookingResponse
{
  public Guid Id { get; init; }
  public string HotelName { get; init; }
  public decimal TotalPrice { get; init; }
  public DateOnly CheckInDateUtc { get; init; }
  public DateOnly CheckOutDateUtc { get; init; }
  public DateOnly BookingDateUtc { get; init; }
}