using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Cities;

/// <param name="SortColumn">Should be empty, 'id', 'name', 'country', or 'postOffice'.</param>
public record CitiesGetRequest(
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