using AutoMapper;
using MediatR;
using TABP.Application.Owners.Common;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Owners.GetById;

public class GetOwnerByIdQueryHandler : IRequestHandler<GetOwnerByIdQuery, OwnerResponse>
{
  private readonly IMapper _mapper;
  private readonly IOwnerRepository _ownerRepository;

  public GetOwnerByIdQueryHandler(IOwnerRepository ownerRepository, IMapper mapper)
  {
    _ownerRepository = ownerRepository;
    _mapper = mapper;
  }

  public async Task<OwnerResponse> Handle(
    GetOwnerByIdQuery request,
    CancellationToken cancellationToken)
  {
    var owner = await _ownerRepository.GetByIdAsync(request.OwnerId, cancellationToken)
                ?? throw new NotFoundException(OwnerMessages.NotFound);

    return _mapper.Map<OwnerResponse>(owner);
  }
}