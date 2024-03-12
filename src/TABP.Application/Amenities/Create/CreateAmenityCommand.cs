using MediatR;
using TABP.Application.Amenities.Common;

namespace TABP.Application.Amenities.Create;

public class CreateAmenityCommand : IRequest<AmenityResponse>
{
  public string Name { get; init; }
  public string? Description { get; init; }
}