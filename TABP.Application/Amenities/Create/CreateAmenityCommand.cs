using MediatR;
using TABP.Application.Amenities.Common;

namespace TABP.Application.Amenities.Create;

public record CreateAmenityCommand(
  string Name,
  string? Description) : IRequest<AmenityResponse>;