using MediatR;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Hotels.AddToGallery;

public class AddImageToHotelGalleryCommandHandler : IRequestHandler<AddImageToHotelGalleryCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IImageRepository _imageRepository;
  private readonly IUnitOfWork _unitOfWork;

  public AddImageToHotelGalleryCommandHandler(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork,
    IHotelRepository hotelRepository)
  {
    _imageRepository = imageRepository;
    _unitOfWork = unitOfWork;
    _hotelRepository = hotelRepository;
  }

  public async Task Handle(AddImageToHotelGalleryCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _hotelRepository.ExistsByIdAsync(request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    await _imageRepository.CreateAsync(
      request.Image,
      request.HotelId,
      ImageType.Gallery,
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}