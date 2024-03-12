using AutoMapper;
using TABP.Application.Hotels.Create;
using TABP.Application.Hotels.GetForGuestById;
using TABP.Application.Hotels.GetForManagement;
using TABP.Application.Hotels.Search;
using TABP.Application.Hotels.Update;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Application.Mapping;

public class HotelProfile : Profile
{
  public HotelProfile()
  {
    CreateMap<PaginatedList<HotelSearchResult>, PaginatedList<HotelSearchResultResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

    CreateMap<PaginatedList<HotelForManagement>, PaginatedList<HotelForManagementResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));

    CreateMap<CreateHotelCommand, Hotel>();
    CreateMap<UpdateHotelCommand, Hotel>();

    CreateMap<Hotel, HotelForGuestResponse>()
      .ForMember(dst => dst.ThumbnailUrl, options => options.MapFrom(
        src => src.Thumbnail != null ? src.Thumbnail.Path : null))
      .ForMember(dst => dst.GalleryUrls, options => options.MapFrom(
        src => src.Gallery.Select(i => i.Path)));

    CreateMap<HotelSearchResult, HotelSearchResultResponse>()
      .ForMember(dst => dst.ThumbnailUrl, options => options.MapFrom(
        src => src.Thumbnail != null ? src.Thumbnail.Path : null));

    CreateMap<HotelForManagement, HotelForManagementResponse>()
      .ForMember(dst => dst.Owner, options => options.MapFrom(src => src.Owner));
  }
}