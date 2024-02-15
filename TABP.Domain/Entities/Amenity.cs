namespace TABP.Domain.Entities;

public class Amenity : EntityBase
{
  public string Name { get; set; }
  public string? Description { get; set; }
  public ICollection<RoomClass> RoomClasses { get; set; } = new List<RoomClass>();
}