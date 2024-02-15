using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Owners;

/// <param name="SortColumn">Should be empty, 'id', 'firstName', or 'lastName'.</param>
public record OwnersGetRequest(
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