using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class Booking : EntityBase
{
  public Guid GuestId { get; set; }
  public User Guest { get; set; }
  public Guid HotelId { get; set; }
  public Hotel Hotel { get; set; }
  public ICollection<Room> Rooms { get; set; } = new List<Room>();
  public ICollection<InvoiceRecord> Invoice { get; set; } = new List<InvoiceRecord>();
  public decimal TotalPrice { get; set; }
  public DateOnly CheckInDateUtc { get; set; }
  public DateOnly CheckOutDateUtc { get; set; }
  public DateOnly BookingDateUtc { get; set; }
  public string? GuestRemarks { get; set; }
  public PaymentMethod PaymentMethod { get; set; }
}