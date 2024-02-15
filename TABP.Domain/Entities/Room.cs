namespace TABP.Domain.Entities;

public class Room : EntityBase, IAuditableEntity
{
  public Guid RoomClassId { get; set; }
  public RoomClass RoomClass { get; set; }
  public string Number { get; set; }
  public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
  public ICollection<InvoiceRecord> InvoiceRecords { get; set; } = new List<InvoiceRecord>();
  public DateTime CreatedAtUtc { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
}