using AutoMapper;
using TABP.Api.Dtos.Cities;
using TABP.Application.Cities.Create;
using TABP.Application.Cities.GetForManagement;
using TABP.Application.Cities.GetTrending;
using TABP.Application.Cities.Update;
using static TABP.Api.Mapping.MappingUtilities;

namespace TABP.Api.Mapping;

public class CityProfile : Profile
{
  public CityProfile()
  {
    CreateMap<CitiesGetRequest, GetCitiesForManagementQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<TrendingCitiesGetRequest, GetTrendingCitiesQuery>();

    CreateMap<CityCreationRequest, CreateCityCommand>();

    CreateMap<CityUpdateRequest, UpdateCityCommand>();
  }
}