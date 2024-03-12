using AutoMapper;
using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Cities.Update;

public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand>
{
  private readonly ICityRepository _cityRepository;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateCityCommandHandler(ICityRepository cityRepository, IUnitOfWork unitOfWork, IMapper mapper)
  {
    _cityRepository = cityRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Handle(UpdateCityCommand request, CancellationToken cancellationToken)
  {
    if (await _cityRepository.ExistsByPostOfficeAsync(request.PostOffice, cancellationToken))
    {
      throw new CityWithPostOfficeExistsException(CityMessages.PostOfficeExists);
    }

    var cityEntity = await _cityRepository.GetByIdAsync(request.CityId, cancellationToken) ??
                     throw new NotFoundException(CityMessages.NotFound);

    _mapper.Map(request, cityEntity);

    await _cityRepository.UpdateAsync(cityEntity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}