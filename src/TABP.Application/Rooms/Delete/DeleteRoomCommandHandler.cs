using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Rooms.Delete;

public class DeleteRoomCommandHandler : IRequestHandler<DeleteRoomCommand>
{
  private readonly IBookingRepository _bookingRepository;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteRoomCommandHandler(
    IRoomRepository roomRepository,
    IUnitOfWork unitOfWork,
    IRoomClassRepository roomClassRepository,
    IBookingRepository bookingRepository)
  {
    _roomRepository = roomRepository;
    _unitOfWork = unitOfWork;
    _roomClassRepository = roomClassRepository;
    _bookingRepository = bookingRepository;
  }

  public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }

    if (!await _roomRepository.ExistsByIdAndRoomClassIdAsync(request.RoomClassId, request.RoomId, cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.RoomNotFound);
    }

    if (await _bookingRepository.ExistsByRoomIdAsync(request.RoomId, cancellationToken))
    {
      throw new DependentsExistException(RoomMessages.DependentsExist);
    }

    await _roomRepository.DeleteAsync(request.RoomId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}