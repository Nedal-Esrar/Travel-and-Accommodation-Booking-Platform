using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Bookings.Delete;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;

  public DeleteBookingCommandHandler(IBookingRepository bookingRepository, IUserRepository userRepository,
    IUnitOfWork unitOfWork)
  {
    _bookingRepository = bookingRepository;
    _userRepository = userRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(
    DeleteBookingCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _userRepository.ExistsByIdAsync(request.GuestId, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }

    if (!await _bookingRepository.ExistsByIdAndGuestIdAsync(request.BookingId, request.GuestId, cancellationToken))
    {
      throw new NotFoundException(BookingMessages.NotFoundForGuest);
    }

    var booking = await _bookingRepository.GetByIdAsync(
      request.BookingId, cancellationToken);

    if (booking!.CheckInDateUtc <= DateOnly.FromDateTime(DateTime.UtcNow))
    {
      throw new CannotCancelBookingException(BookingMessages.CheckedIn);
    }

    await _bookingRepository.DeleteAsync(request.BookingId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}