using MediatR;
using TABP.Domain;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Interfaces.Services;
using TABP.Domain.Messages;

namespace TABP.Application.Bookings.Delete;

public class DeleteBookingCommandHandler : IRequestHandler<DeleteBookingCommand>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserContext _userContext;
  private readonly IUserRepository _userRepository;

  public DeleteBookingCommandHandler(
    IBookingRepository bookingRepository, 
    IUserContext userContext,
    IUnitOfWork unitOfWork,
    IUserRepository userRepository)
  {
    _bookingRepository = bookingRepository;
    _userContext = userContext;
    _unitOfWork = unitOfWork;
    _userRepository = userRepository;
  }

  public async Task Handle(
    DeleteBookingCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _userRepository.ExistsByIdAsync(_userContext.Id, cancellationToken))
    {
      throw new NotFoundException(UserMessages.NotFound);
    }
    
    if (_userContext.Role != UserRoles.Guest)
    {
      throw new ForbiddenException(UserMessages.NotGuest);
    }

    if (!await _bookingRepository.ExistsAsync(
          b => b.Id == request.BookingId && b.GuestId == _userContext.Id,
          cancellationToken))
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