using AutoMapper;
using MediatR;
using TABP.Application.Owners.Common;
using TABP.Domain.Entities;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;

namespace TABP.Application.Owners.Create;

public class CreateOwnerCommandHandler : IRequestHandler<CreateOwnerCommand, OwnerResponse>
{
  private readonly IMapper _mapper;
  private readonly IOwnerRepository _ownerRepository;
  private readonly IUnitOfWork _unitOfWork;

  public CreateOwnerCommandHandler(
    IOwnerRepository ownerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _ownerRepository = ownerRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task<OwnerResponse> Handle(
    CreateOwnerCommand request,
    CancellationToken cancellationToken = default)
  {
    var createdOwner = await _ownerRepository.CreateAsync(
      _mapper.Map<Owner>(request),
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return _mapper.Map<OwnerResponse>(createdOwner);
  }
}