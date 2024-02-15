namespace TABP.Application.Cities.GetForManagement;

public record CityForManagementResponse(
  Guid Id,
  string Name,
  string Country,
  string PostOffice,
  int NumberOfHotels,
  DateTime CreatedAtUtc,
  DateTime? ModifiedAtUtc);