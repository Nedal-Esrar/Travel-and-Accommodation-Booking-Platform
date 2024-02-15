using AutoMapper;
using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Owners.Update;

public class UpdateOwnerCommandHandler : IRequestHandler<UpdateOwnerCommand>
{
  private readonly IMapper _mapper;
  private readonly IOwnerRepository _ownerRepository;
  private readonly IUnitOfWork _unitOfWork;

  public UpdateOwnerCommandHandler(
    IOwnerRepository ownerRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper)
  {
    _ownerRepository = ownerRepository;
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }

  public async Task Handle(UpdateOwnerCommand request, CancellationToken cancellationToken = default)
  {
    var ownerEntity = await _ownerRepository.GetByIdAsync(request.OwnerId, cancellationToken)
                      ?? throw new NotFoundException(OwnerMessages.NotFound);

    _mapper.Map(request, ownerEntity);

    await _ownerRepository.UpdateAsync(ownerEntity, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}