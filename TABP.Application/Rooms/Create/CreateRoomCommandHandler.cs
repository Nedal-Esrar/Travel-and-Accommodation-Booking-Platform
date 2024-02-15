using AutoMapper;
using MediatR;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Rooms.Create;

public class CreateRoomHandler : IRequestHandler<CreateRoomCommand, Guid>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateRoomHandler(
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

  public async Task<Guid> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }

    if (await _roomRepository.ExistsByNumberInRoomClassAsync(request.Number, request.RoomClassId, cancellationToken))
    {
      throw new RoomWithNumberExistsInRoomClassException(RoomClassMessages.DuplicatedRoomNumber);
    }

    var createdRoom = await _roomRepository.CreateAsync(
      _mapper.Map<Room>(request),
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return createdRoom.Id;
  }
}