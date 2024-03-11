using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.RoomClasses;

/// <param name="SortColumn">Should be empty, 'id', 'name', 'adultsCapacity', 'childrenCapacity', or 'price'.</param>
public class RoomClassesGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}