using MediatR;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Hotels.SetThumbnail;

public class SetHotelThumbnailCommandHandler : IRequestHandler<SetHotelThumbnailCommand>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IImageRepository _imageRepository;
  private readonly IUnitOfWork _unitOfWork;

  public SetHotelThumbnailCommandHandler(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork,
    IHotelRepository hotelRepository)
  {
    _imageRepository = imageRepository;
    _unitOfWork = unitOfWork;
    _hotelRepository = hotelRepository;
  }

  public async Task Handle(SetHotelThumbnailCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    await _imageRepository.DeleteForAsync(
      request.HotelId,
      ImageType.Thumbnail,
      cancellationToken);

    await _imageRepository.CreateAsync(
      request.Image,
      request.HotelId,
      ImageType.Thumbnail,
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}