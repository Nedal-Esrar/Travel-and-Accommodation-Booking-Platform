using AutoMapper;
using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Rooms.Update;

public class UpdateRoomCommandHandler : IRequestHandler<UpdateRoomCommand>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateRoomCommandHandler(
    IRoomRepository roomRepository,
    IRoomClassRepository roomClassRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork)
  {
    _roomRepository = roomRepository;
    _roomClassRepository = roomClassRepository;
    _mapper = mapper;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }

    if (await _roomRepository.ExistsByNumberInRoomClassAsync(request.Number, request.RoomClassId, cancellationToken))
    {
      throw new RoomWithNumberExistsInRoomClassException(RoomClassMessages.DuplicatedRoomNumber);
    }

    var roomEntity = await _roomRepository.GetByIdAsync(
      request.RoomClassId, request.RoomId,
      cancellationToken);

    if (roomEntity is null)
    {
      throw new NotFoundException(RoomClassMessages.RoomNotFound);
    }

    _mapper.Map(request, roomEntity);

    await _roomRepository.UpdateAsync(roomEntity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}