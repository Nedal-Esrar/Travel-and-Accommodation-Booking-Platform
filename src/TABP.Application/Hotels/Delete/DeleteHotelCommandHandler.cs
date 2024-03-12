using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Hotels.Delete;

public class DeleteHotelCommandHandler : IRequestHandler<DeleteHotelCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;

  public DeleteHotelCommandHandler(
    IHotelRepository hotelRepository,
    IUnitOfWork unitOfWork,
    IRoomClassRepository roomClassRepository)
  {
    _hotelRepository = hotelRepository;
    _unitOfWork = unitOfWork;
    _roomClassRepository = roomClassRepository;
  }

  public async Task Handle(DeleteHotelCommand request, CancellationToken cancellationToken = default)
  {
    if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    if (await _roomClassRepository.ExistsByHotelIdAsync(request.HotelId, cancellationToken))
    {
      throw new DependentsExistException(HotelMessages.DependentsExist);
    }

    await _hotelRepository.DeleteAsync(request.HotelId, cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}