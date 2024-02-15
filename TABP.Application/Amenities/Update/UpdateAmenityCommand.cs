using MediatR;

namespace TABP.Application.Amenities.Update;

public record UpdateAmenityCommand(
  Guid AmenityId,
  string Name,
  string? Description) : IRequest;