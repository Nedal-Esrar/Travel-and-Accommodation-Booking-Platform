using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Discounts.Delete;

public class DeleteDiscountCommandHandler : IRequestHandler<DeleteDiscountCommand>
{
  private readonly IDiscountRepository _discountRepository;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteDiscountCommandHandler(
    IRoomClassRepository roomClassRepository,
    IDiscountRepository discountRepository,
    IUnitOfWork unitOfWork)
  {
    _roomClassRepository = roomClassRepository;
    _discountRepository = discountRepository;
    _unitOfWork = unitOfWork;
  }

  public async Task Handle(DeleteDiscountCommand request, CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsByIdAsync(
          request.RoomClassId,
          cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }

    if (!await _discountRepository.ExistsByIdAsync(request.RoomClassId, request.DiscountId, cancellationToken))
    {
      throw new NotFoundException(DiscountMessages.NotFoundInRoomClass);
    }

    await _discountRepository.DeleteAsync(request.DiscountId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}