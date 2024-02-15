using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Discounts;

/// <param name="SortColumn">Should be empty, 'id', 'creaationDate', 'startDate', or 'endDate'.</param>
public record DiscountsGetRequest(
  string? SortColumn,
  string? SortOrder,
  int PageNumber = 1,
  int PageSize = 10) : ResourcesQueryRequest(
  null, SortColumn, SortOrder, PageNumber, PageSize);