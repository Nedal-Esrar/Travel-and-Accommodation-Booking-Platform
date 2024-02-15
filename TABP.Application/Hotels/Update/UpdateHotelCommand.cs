using MediatR;

namespace TABP.Application.Hotels.Update;

public record UpdateHotelCommand(
  Guid HotelId,
  Guid CityId,
  Guid OwnerId,
  string Name,
  int StarRating,
  double Longitude,
  double Latitude,
  string? BriefDescription,
  string? Description,
  string PhoneNumber) : IRequest;