using TABP.Domain.Entities;

namespace TABP.Domain.Models;

public class HotelForManagement
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public int StarRating { get; set; }
  public Owner Owner { get; set; }
  public int NumberOfRooms { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
}