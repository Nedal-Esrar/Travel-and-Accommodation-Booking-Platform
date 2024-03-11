using TABP.Application.Owners.Common;

namespace TABP.Application.Hotels.GetForManagement;

public class HotelForManagementResponse
{
  public Guid Id { get; init; }
  public string Name { get; init; }
  public int StarRating { get; init; }
  public OwnerResponse Owner { get; init; }
  public int NumberOfRooms { get; init; }
  public DateTime CreatedAtUtc { get; init; }
  public DateTime? ModifiedAtUtc { get; init; }
}