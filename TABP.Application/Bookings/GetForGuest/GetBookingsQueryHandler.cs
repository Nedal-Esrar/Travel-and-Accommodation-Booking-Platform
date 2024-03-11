using AutoMapper;
using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;
using TABP.Domain.Models;

namespace TABP.Application.Bookings.GetForGuest;

public class GetBookingsQueryHandler : IRequestHandler<GetBookingsQuery, PaginatedList<BookingResponse>>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IMapper _mapper;
  private readonly IUserRepository _userRepository;

  public GetBookingsQueryHandler(
    IUserRepository userRepository,
    IBookingRepository bookingRepository,
    IMapper mapper)
  {
    _userRepository = userRepository;
    _bookingRepository = bookingRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedList<BookingResponse>> Handle(GetBookingsQuery request,
    CancellationToken cancellationToken = default)
  {
    if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }

    var query = new PaginationQuery<Booking>(
      b => b.GuestId == request.GuestId,
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var bookings = await _bookingRepository.GetAsync(query, cancellationToken);

    return _mapper.Map<PaginatedList<BookingResponse>>(bookings);
  }
}