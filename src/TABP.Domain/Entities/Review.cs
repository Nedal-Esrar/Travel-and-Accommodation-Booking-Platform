namespace TABP.Domain.Entities;

public class Review : EntityBase, IAuditableEntity
{
  public Guid GuestId { get; set; }
  public User Guest { get; set; }
  public Guid HotelId { get; set; }
  public Hotel Hotel { get; set; }
  public string Content { get; set; }
  public int Rating { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
}