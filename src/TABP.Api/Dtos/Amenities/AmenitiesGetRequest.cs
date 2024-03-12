using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Amenities;

/// <param name="SortColumn">Should be empty, 'id', or 'name'.</param>
public class AmenitiesGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}