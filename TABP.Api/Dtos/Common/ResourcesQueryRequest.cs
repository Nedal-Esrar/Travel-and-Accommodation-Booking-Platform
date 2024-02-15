namespace TABP.Api.Dtos.Common;

public record ResourcesQueryRequest
{
  private const int MaxPageSize = 20;

  protected ResourcesQueryRequest(string? searchTerm, string? sortOrder, string? sortColumn, int pageNumber,
    int pageSize)
  {
    SearchTerm = searchTerm;
    SortOrder = sortOrder;
    SortColumn = sortColumn;
    PageNumber = pageNumber;
    PageSize = Math.Min(pageSize, MaxPageSize);
  }

  public string? SearchTerm { get; }

  /// <summary>
  ///   Should be empty, 'asc', or 'desc'
  /// </summary>
  public string? SortOrder { get; }

  public string? SortColumn { get; }
  public int PageNumber { get; }
  public int PageSize { get; }
}