namespace TABP.Domain.Entities;

public class Hotel : EntityBase, IAuditableEntity
{
  public Guid CityId { get; set; }
  public City City { get; set; }
  public Guid OwnerId { get; set; }
  public Owner Owner { get; set; }
  public Image? Thumbnail { get; set; }
  public ICollection<Image> Gallery { get; set; } = new List<Image>();
  public ICollection<RoomClass> RoomClasses { get; set; } = new List<RoomClass>();
  public ICollection<Booking> Bookings { get; set; } = new List<Booking>();
  public ICollection<Review> Reviews { get; set; } = new List<Review>();
  public string Name { get; set; }
  public double ReviewsRating { get; set; }
  public int StarRating { get; set; }
  public double Longitude { get; set; }
  public double Latitude { get; set; }
  public string? BriefDescription { get; set; }
  public string? Description { get; set; }
  public string PhoneNumber { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
}