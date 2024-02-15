using MediatR;
using TABP.Application.Amenities.Common;

namespace TABP.Application.Amenities.GetById;

public record GetAmenityByIdQuery(Guid AmenityId) : IRequest<AmenityResponse>;