using MediatR;
using TABP.Application.Owners.Common;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Owners.Get;

public class GetOwnersQuery : IRequest<PaginatedList<OwnerResponse>>
{
  public string? SearchTerm { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}