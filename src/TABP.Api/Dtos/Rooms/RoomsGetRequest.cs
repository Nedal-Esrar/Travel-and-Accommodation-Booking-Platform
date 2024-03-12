using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Rooms;

/// <param name="SortColumn">Should be empty, 'id', or 'number'.</param>
public class RoomsGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}