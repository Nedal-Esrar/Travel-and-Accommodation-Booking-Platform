using AutoMapper;
using TABP.Api.Dtos.Images;
using TABP.Application.Cities.SetThumbnail;
using TABP.Application.Hotels.AddToGallery;
using TABP.Application.Hotels.SetThumbnail;
using TABP.Application.RoomClasses.AddImageToGallery;

namespace TABP.Api.Mapping;

public class ImageProfile : Profile
{
  public ImageProfile()
  {
    CreateMap<ImageCreationRequest, SetCityThumbnailCommand>();
    CreateMap<ImageCreationRequest, SetHotelThumbnailCommand>();
    CreateMap<ImageCreationRequest, AddImageToHotelGalleryCommand>();
    CreateMap<ImageCreationRequest, AddImageToRoomClassGalleryCommand>();
  }
}