using AutoMapper;
using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Hotels.GetRecentlyVisited;

public class GetRecentlyVisitedHotelsQueryHandler : IRequestHandler<GetRecentlyVisitedHotelsForGuestQuery,
  IEnumerable<RecentlyVisitedHotelResponse>>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IMapper _mapper;
  private readonly IUserRepository _userRepository;

  public GetRecentlyVisitedHotelsQueryHandler(
    IUserRepository userRepository,
    IBookingRepository bookingRepository,
    IMapper mapper)
  {
    _userRepository = userRepository;
    _bookingRepository = bookingRepository;
    _mapper = mapper;
  }

  public async Task<IEnumerable<RecentlyVisitedHotelResponse>> Handle(
    GetRecentlyVisitedHotelsForGuestQuery request,
    CancellationToken cancellationToken = default)
  {
    if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }

    var recentBookingsInDifferentHotels =
      await _bookingRepository.GetRecentBookingsInDifferentHotelsByGuestId(
        request.GuestId, request.Count, cancellationToken);

    return _mapper.Map<IEnumerable<RecentlyVisitedHotelResponse>>(recentBookingsInDifferentHotels);
  }
}