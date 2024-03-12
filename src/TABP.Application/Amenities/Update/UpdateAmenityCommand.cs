using MediatR;

namespace TABP.Application.Amenities.Update;

public class UpdateAmenityCommand : IRequest
{
  public Guid AmenityId { get; init; }
  public string Name { get; init; }

  public string? Description { get; init; }
}