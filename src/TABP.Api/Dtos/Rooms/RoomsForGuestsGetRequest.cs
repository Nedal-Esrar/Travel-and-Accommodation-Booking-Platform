using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Rooms;

public class RoomsForGuestsGetRequest : ResourcesQueryRequest
{
  public DateOnly CheckInDateUtc { get; init; }
  public DateOnly CheckOutDateUtc { get; init; }
}