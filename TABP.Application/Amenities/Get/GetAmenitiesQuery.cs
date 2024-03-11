using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Amenities.Get;

public class GetAmenitiesQuery : IRequest<PaginatedList<AmenityResponse>>
{
  public string? SearchTerm { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}