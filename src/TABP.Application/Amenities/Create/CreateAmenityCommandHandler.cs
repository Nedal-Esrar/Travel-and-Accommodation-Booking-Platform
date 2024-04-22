using AutoMapper;
using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Domain.Entities;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Amenities.Create;

public class CreateAmenityCommandHandler : IRequestHandler<CreateAmenityCommand, AmenityResponse>
{
  private readonly IAmenityRepository _amenityRepository;
  private readonly IMapper _mapper;
  private readonly IUnitOfWork _unitOfWork;

  public CreateAmenityCommandHandler(
    IAmenityRepository amenityRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _amenityRepository = amenityRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<AmenityResponse> Handle(
    CreateAmenityCommand request,
    CancellationToken cancellationToken = default)
  {
    if (await _amenityRepository.ExistsAsync(a => a.Name == request.Name, cancellationToken))
    {
      throw new AmenityExistsException(AmenityMessages.WithNameExists);
    }

    var createdAmenity = await _amenityRepository.CreateAsync(
      _mapper.Map<Amenity>(request),
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return _mapper.Map<AmenityResponse>(createdAmenity);
  }
}