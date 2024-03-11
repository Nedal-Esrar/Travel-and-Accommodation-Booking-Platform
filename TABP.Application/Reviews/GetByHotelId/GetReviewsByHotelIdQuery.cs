using MediatR;
using TABP.Application.Reviews.Common;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Reviews.GetByHotelId;

public class GetReviewsByHotelIdQuery : IRequest<PaginatedList<ReviewResponse>>
{
  public Guid HotelId { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}