using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Cities;

/// <param name="SortColumn">Should be empty, 'id', 'name', 'country', or 'postOffice'.</param>
public class CitiesGetRequest : ResourcesQueryRequest
{
  public string? SearchTerm { get; init; }
}