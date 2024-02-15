using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Cities.Delete;

public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand>
{
  private readonly ICityRepository _cityRepository;
  private readonly IHotelRepository _hotelRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteCityCommandHandler(
    ICityRepository cityRepository,
    IUnitOfWork unitOfWork,
    IHotelRepository hotelRepository)
  {
    _cityRepository = cityRepository;
    _unitOfWork = unitOfWork;
    _hotelRepository = hotelRepository;
  }

  public async Task Handle(DeleteCityCommand request, CancellationToken cancellationToken)
  {
    if (!await _cityRepository.ExistsByIdAsync(request.CityId, cancellationToken))
    {
      throw new NotFoundException(CityMessages.NotFound);
    }

    if (await _hotelRepository.ExistsByCityIdAsync(request.CityId, cancellationToken))
    {
      throw new DependentsExistException(CityMessages.DependentsExist);
    }

    await _cityRepository.DeleteAsync(request.CityId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}