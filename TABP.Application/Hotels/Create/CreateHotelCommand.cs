using MediatR;

namespace TABP.Application.Hotels.Create;

public record CreateHotelCommand(
  Guid CityId,
  Guid OwnerId,
  string Name,
  int StarRating,
  double Longitude,
  double Latitude,
  string? BriefDescription,
  string? Description,
  string PhoneNumber) : IRequest<Guid>;