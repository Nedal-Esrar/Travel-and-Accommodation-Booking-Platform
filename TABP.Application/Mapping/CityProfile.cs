using AutoMapper;
using TABP.Application.Cities.Create;
using TABP.Application.Cities.GetForManagement;
using TABP.Application.Cities.GetTrending;
using TABP.Application.Cities.Update;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Application.Mapping;

public class CityProfile : Profile
{
  public CityProfile()
  {
    CreateMap<PaginatedList<CityForManagement>, PaginatedList<CityForManagementResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<CreateCityCommand, City>();
    CreateMap<UpdateCityCommand, City>();
    CreateMap<City, CityResponse>();
    CreateMap<City, TrendingCityResponse>()
      .ForMember(dst => dst.ThumbnailUrl, options => options.MapFrom(
        src => src.Thumbnail != null ? src.Thumbnail.Path : null));
    CreateMap<CityForManagement, CityForManagementResponse>();
  }
}