using MediatR;
using TABP.Application.Owners.Common;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Owners.Get;

public record GetOwnersQuery(
  string? SearchTerm,
  SortOrder? SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize) : IRequest<PaginatedList<OwnerResponse>>;