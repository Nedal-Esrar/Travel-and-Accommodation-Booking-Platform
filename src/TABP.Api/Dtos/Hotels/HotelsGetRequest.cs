using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Hotels;

/// <param name="SortColumn">Should be empty, 'id', or 'name'.</param>
public class HotelsGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}