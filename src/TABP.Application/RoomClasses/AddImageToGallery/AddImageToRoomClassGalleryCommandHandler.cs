using MediatR;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.RoomClasses.AddImageToGallery;

public class AddImageToRoomClassGalleryCommandHandler : IRequestHandler<AddImageToRoomClassGalleryCommand>
{
  private readonly IImageRepository _imageRepository;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IUnitOfWork _unitOfWork;

  public AddImageToRoomClassGalleryCommandHandler(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork,
    IRoomClassRepository roomClassRepository)
  {
    _imageRepository = imageRepository;
    _unitOfWork = unitOfWork;
    _roomClassRepository = roomClassRepository;
  }

  public async Task Handle(AddImageToRoomClassGalleryCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }

    await _imageRepository.CreateAsync(
      request.Image,
      request.RoomClassId,
      ImageType.Gallery,
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}