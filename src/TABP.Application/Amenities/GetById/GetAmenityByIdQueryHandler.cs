using AutoMapper;
using MediatR;
using TABP.Application.Amenities.Common;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Amenities.GetById;

public class GetAmenityByIdQueryHandler : IRequestHandler<GetAmenityByIdQuery, AmenityResponse>
{
  private readonly IAmenityRepository _amenityRepository;
  private readonly IMapper _mapper;

  public GetAmenityByIdQueryHandler(IAmenityRepository amenityRepository, IMapper mapper)
  {
    _amenityRepository = amenityRepository;
    _mapper = mapper;
  }

  public async Task<AmenityResponse> Handle(
    GetAmenityByIdQuery request,
    CancellationToken cancellationToken = default)
  {
    var amenity = await _amenityRepository.GetByIdAsync(
                    request.AmenityId,
                    cancellationToken) ??
                  throw new NotFoundException(AmenityMessages.WithIdNotFound);

    return _mapper.Map<AmenityResponse>(amenity);
  }
}