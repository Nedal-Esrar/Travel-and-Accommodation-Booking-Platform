namespace TABP.Domain.Models;

public class CityForManagement
{
  public Guid Id { get; set; }
  public string Name { get; set; }
  public string Country { get; set; }
  public string PostOffice { get; set; }
  public int NumberOfHotels { get; set; }
  public DateTime CreatedAtUtc { get; set; }
  public DateTime? ModifiedAtUtc { get; set; }
}