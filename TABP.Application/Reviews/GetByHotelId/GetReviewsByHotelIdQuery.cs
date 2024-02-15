using MediatR;
using TABP.Application.Reviews.Common;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Reviews.GetByHotelId;

public record GetReviewsByHotelIdQuery(
  Guid HotelId,
  SortOrder? SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize) : IRequest<PaginatedList<ReviewResponse>>;