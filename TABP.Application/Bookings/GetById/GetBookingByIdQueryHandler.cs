using AutoMapper;
using MediatR;
using TABP.Application.Bookings.Common;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Bookings.GetById;

public class GetBookingByIdQueryHandler : IRequestHandler<GetBookingByIdQuery, BookingResponse>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IMapper _mapper;
  private readonly IUserRepository _userRepository;

  public GetBookingByIdQueryHandler(IUserRepository userRepository, IBookingRepository bookingRepository,
    IMapper mapper)
  {
    _userRepository = userRepository;
    _bookingRepository = bookingRepository;
    _mapper = mapper;
  }

  public async Task<BookingResponse> Handle(
    GetBookingByIdQuery request,
    CancellationToken cancellationToken = default)
  {
    if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }

    var booking = await _bookingRepository.GetByIdAsync(
                    request.GuestId,
                    request.BookingId,
                    false,
                    cancellationToken) ??
                  throw new NotFoundException(BookingMessages.NotFoundForGuest);

    return _mapper.Map<BookingResponse>(booking);
  }
}