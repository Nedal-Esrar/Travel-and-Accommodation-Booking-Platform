using MediatR;
using TABP.Application.Amenities.Common;

namespace TABP.Application.Amenities.GetById;

public class GetAmenityByIdQuery : IRequest<AmenityResponse>
{
  public Guid AmenityId { get; init; }
}