using AutoMapper;
using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Hotels.Update;

public class UpdateHotelCommandHandler : IRequestHandler<UpdateHotelCommand>
{
  private readonly ICityRepository _cityRepository;
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IOwnerRepository _ownerRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateHotelCommandHandler(
    ICityRepository cityRepository,
    IOwnerRepository ownerRepository,
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _cityRepository = cityRepository;
    _ownerRepository = ownerRepository;
    _hotelRepository = hotelRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Handle(UpdateHotelCommand request, CancellationToken cancellationToken = default)
  {
    var hotelEntity = await _hotelRepository.GetByIdAsync(
                        request.HotelId,
                        false,
                        false,
                        false,
                        cancellationToken)
                      ?? throw new NotFoundException(HotelMessages.NotFound);

    if (!await _cityRepository.ExistsByIdAsync(request.CityId, cancellationToken))
    {
      throw new NotFoundException(CityMessages.NotFound);
    }

    if (!await _ownerRepository.ExistsByIdAsync(request.OwnerId, cancellationToken))
    {
      throw new NotFoundException(OwnerMessages.NotFound);
    }

    if (await _hotelRepository.ExistsByLocation(request.Longitude, request.Latitude))
    {
      throw new HotelLocationReplicationException(HotelMessages.SameLocationExists);
    }

    _mapper.Map(request, hotelEntity);

    await _hotelRepository.UpdateAsync(hotelEntity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}