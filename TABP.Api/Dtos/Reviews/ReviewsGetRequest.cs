using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Reviews;

/// <param name="SortColumn">Should be empty, 'id', or 'rating'.</param>
public record ReviewsGetRequest(
  string? SortOrder,
  string? SortColumn,
  int PageNumber = 1,
  int PageSize = 10) : ResourcesQueryRequest(
  null,
  SortOrder,
  SortColumn,
  PageNumber,
  PageSize);