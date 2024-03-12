namespace TABP.Application.Cities.GetForManagement;

public class CityForManagementResponse
{
  public Guid Id { get; init; }
  public string Name { get; init; }
  public string Country { get; init; }
  public string PostOffice { get; init; }
  public int NumberOfHotels { get; init; }
  public DateTime CreatedAtUtc { get; init; }
  public DateTime? ModifiedAtUtc { get; init; }
}