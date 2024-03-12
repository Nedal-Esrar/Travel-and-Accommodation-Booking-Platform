using AutoMapper;
using MediatR;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.RoomClasses.Create;

public class CreateRoomClassCommandHandler : IRequestHandler<CreateRoomClassCommand, Guid>
{
  private readonly IAmenityRepository _amenityRepository;
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateRoomClassCommandHandler(
    IHotelRepository hotelRepository,
    IRoomClassRepository roomClassRepository,
    IAmenityRepository amenityRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _roomClassRepository = roomClassRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
    _amenityRepository = amenityRepository;
  }

  public async Task<Guid> Handle(
    CreateRoomClassCommand request,
    CancellationToken cancellationToken)
  {
    if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    if (await _roomClassRepository.ExistsByNameInHotelAsync(request.HotelId, request.Name, cancellationToken))
    {
      throw new RoomClassWithSameNameFoundException(RoomClassMessages.NameInHotelFound);
    }

    foreach (var amenityId in request.AmenitiesIds)
    {
      if (!await _amenityRepository.ExistsByIdAsync(amenityId, cancellationToken))
      {
        throw new NotFoundException(AmenityMessages.WithIdNotFound);
      }
    }

    var roomClass = _mapper.Map<RoomClass>(request);
    
    foreach (var amenityId in request.AmenitiesIds)
    {
      roomClass.Amenities
        .Add((await _amenityRepository.GetByIdAsync(amenityId, cancellationToken))!);
    }

    var createdRoomClass = await _roomClassRepository.CreateAsync(
      roomClass, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return createdRoomClass.Id;
  }
}