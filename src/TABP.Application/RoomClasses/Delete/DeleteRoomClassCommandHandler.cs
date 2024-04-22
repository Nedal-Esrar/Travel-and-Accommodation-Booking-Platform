using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.RoomClasses.Delete;

public class DeleteRoomClassCommandHandler : IRequestHandler<DeleteRoomClassCommand>
{
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteRoomClassCommandHandler(IRoomClassRepository roomClassRepository, IUnitOfWork unitOfWork,
    IRoomRepository roomRepository)
  {
    _roomClassRepository = roomClassRepository;
    _unitOfWork = unitOfWork;
    _roomRepository = roomRepository;
  }

  public async Task Handle(DeleteRoomClassCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }

    if (await _roomRepository.ExistsAsync(r => r.RoomClassId == request.RoomClassId, cancellationToken))
    {
      throw new DependentsExistException(RoomClassMessages.DependentsExist);
    }

    await _roomClassRepository.DeleteAsync(request.RoomClassId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}