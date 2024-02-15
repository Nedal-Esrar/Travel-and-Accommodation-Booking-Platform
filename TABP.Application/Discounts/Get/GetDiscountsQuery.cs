using MediatR;
using TABP.Application.Discounts.GetById;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Discounts.Get;

public record GetDiscountsQuery(
  Guid RoomClassId,
  SortOrder? SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize) : IRequest<PaginatedList<DiscountResponse>>;