using TABP.Api.Dtos.Common;

namespace TABP.Api.Dtos.Bookings;

public record BookingsGetRequest(
  int PageNumber = 1,
  int PageSize = 10) : ResourcesQueryRequest(
  null, null, null,
  PageNumber, PageSize);