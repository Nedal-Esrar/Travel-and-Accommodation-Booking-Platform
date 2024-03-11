using MediatR;
using TABP.Application.Discounts.GetById;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Discounts.Get;

public class GetDiscountsQuery : IRequest<PaginatedList<DiscountResponse>>
{
  public Guid RoomClassId { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}