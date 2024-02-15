using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Amenities.Get;

public record GetAmenitiesQuery(
  string? SearchTerm,
  SortOrder? SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize) : IRequest<PaginatedList<AmenityResponse>>;