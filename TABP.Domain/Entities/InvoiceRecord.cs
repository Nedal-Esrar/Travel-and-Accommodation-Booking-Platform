namespace TABP.Domain.Entities;

public class InvoiceRecord : EntityBase
{
  public Guid BookingId { get; set; }
  public Booking Booking { get; set; }
  public Guid RoomId { get; set; }
  public string RoomClassName { get; set; }
  public string RoomNumber { get; set; }
  public decimal PriceAtBooking { get; set; }
  public decimal? DiscountPercentageAtBooking { get; set; }
}