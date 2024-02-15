using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Rooms;

/// <param name="SortColumn">Should be empty, 'id', or 'number'.</param>
public record RoomsGetRequest(
  string? SearchTerm,
  string? SortOrder,
  string? SortColumn,
  int PageNumber = 1,
  int PageSize = 10) : ResourcesQueryRequest(SearchTerm, SortOrder,
  SortColumn, PageNumber, PageSize);