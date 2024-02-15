using TABP.Domain.Enums;

namespace TABP.Domain.Entities;

public class RoomClass : EntityBase, IAuditableEntity
{
  public Guid HotelId { get; set; }
  public Hotel Hotel { get; set; }
  public string Name { get; set; }
  public string? Description { get; set; }
  public int AdultsCapacity { get; set; }
  public int ChildrenCapacity { get; set; }
  public decimal PricePerNight { get; set; }
  public RoomType RoomType { get; set; }
  public ICollection<Room> Rooms { get; set; } = new List<Room>();
  public ICollection<Amenity> Amenities { get; set; } = new List<Amenity>();
  public ICollection<Image> Gallery { get; set; } = new List<Image>();
  public ICollection<Discount> Discounts { get; set; } = new List<Discount>();
  public DateTime CreatedAtUtc { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
}