using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Rooms;

public record RoomsForGuestsGetRequest(
  DateOnly CheckInDateUtc,
  DateOnly CheckOutDateUtc,
  int PageNumber = 1,
  int PageSize = 10) : ResourcesQueryRequest(null, null, null, PageNumber, PageSize);