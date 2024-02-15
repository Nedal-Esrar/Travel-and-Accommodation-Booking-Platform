﻿using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Amenities;

/// <param name="SortColumn">Should be empty, 'id', or 'name'.</param>
public record AmenitiesGetRequest(
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