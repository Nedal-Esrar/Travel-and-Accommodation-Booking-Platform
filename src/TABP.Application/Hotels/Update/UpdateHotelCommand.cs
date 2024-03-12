using MediatR;

namespace TABP.Application.Hotels.Update;

public class UpdateHotelCommand : IRequest
{
  public Guid HotelId { get; init; }
  public Guid CityId { get; init; }
  public Guid OwnerId { get; init; }
  public string Name { get; init; }
  public int StarRating { get; init; }
  public double Longitude { get; init; }
  public double Latitude { get; init; }
  public string? BriefDescription { get; init; }
  public string? Description { get; init; }
  public string PhoneNumber { get; init; }
}