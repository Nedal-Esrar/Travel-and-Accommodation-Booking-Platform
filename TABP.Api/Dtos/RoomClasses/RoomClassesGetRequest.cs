using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.RoomClasses;

/// <param name="SortColumn">Should be empty, 'id', 'name', 'adultsCapacity', 'childrenCapacity', or 'price'.</param>
public record RoomClassesGetRequest(
  string? SearchTerm,
  string? SortOrder,
  string? SortColumn,
  int PageNumber = 1,
  int PageSize = 10) : ResourcesQueryRequest(
  SearchTerm,
  SortOrder,
  SortColumn,
  PageNumber,
  PageSize);