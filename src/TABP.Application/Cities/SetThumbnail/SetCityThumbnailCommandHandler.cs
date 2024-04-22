using MediatR;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Cities.SetThumbnail;

public class SetCityThumbnailCommandHandler : IRequestHandler<SetCityThumbnailCommand>
{
  private readonly ICityRepository _cityRepository;
  private readonly IImageRepository _imageRepository;
  private readonly IUnitOfWork _unitOfWork;

  public SetCityThumbnailCommandHandler(
    IImageRepository imageRepository,
    IUnitOfWork unitOfWork,
    ICityRepository cityRepository)
  {
    _imageRepository = imageRepository;
    _unitOfWork = unitOfWork;
    _cityRepository = cityRepository;
  }

  public async Task Handle(SetCityThumbnailCommand request,
    CancellationToken cancellationToken = default)
  {
    if (!await _cityRepository.ExistsAsync(c => c.Id == request.CityId, cancellationToken))
    {
      throw new NotFoundException(CityMessages.NotFound);
    }

    await _imageRepository.DeleteForAsync(
      request.CityId,
      ImageType.Thumbnail,
      cancellationToken);

    await _imageRepository.CreateAsync(
      request.Image,
      request.CityId,
      ImageType.Thumbnail,
      cancellationToken);

    await _unitOfWork.SaveChangesAsync(cancellationToken);
  }
}