using MediatR;

namespace TABP.Application.Hotels.GetRecentlyVisited;

public record GetRecentlyVisitedHotelsForGuestQuery(
  Guid GuestId,
  int Count) : IRequest<IEnumerable<RecentlyVisitedHotelResponse>>;