using System.Text.Json.Serialization;

namespace TABP.Api.Dtos.Common;

public class ResourcesQueryRequest
{
  private const int MaxPageSize = 20;

  private int _pageSize = 10;

  public string? SearchTerm { get; init; }

  /// <summary>
  ///   Should be empty, 'asc', or 'desc'
  /// </summary>
  public string? SortOrder { get; init; }

  public string? SortColumn { get; init; }
  public int PageNumber { get; init; } = 1;

  public int PageSize
  {
    get => _pageSize;
    init => _pageSize = Math.Min(value, MaxPageSize);
  }
}