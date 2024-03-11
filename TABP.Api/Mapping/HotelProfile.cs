using AutoMapper;
using TABP.Api.Dtos.Hotels;
using TABP.Application.Hotels.Create;
using TABP.Application.Hotels.GetFeaturedDeals;
using TABP.Application.Hotels.GetForManagement;
using TABP.Application.Hotels.GetRecentlyVisited;
using TABP.Application.Hotels.Search;
using TABP.Application.Hotels.Update;
using TABP.Domain.Enums;
using static TABP.Api.Mapping.MappingUtilities;

namespace TABP.Api.Mapping;

public class HotelProfile : Profile
{
  public HotelProfile()
  {
    CreateMap<RecentlyVisitedHotelsGetRequest, GetRecentlyVisitedHotelsForGuestQuery>();
    
    CreateMap<HotelsGetRequest, GetHotelsForManagementQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<HotelSearchRequest, SearchForHotelsQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)))
      .ForMember(dst => dst.RoomTypes, opt => opt.MapFrom(src => src.RoomTypes ?? Enumerable.Empty<RoomType>()))
      .ForMember(dst => dst.Amenities, opt => opt.MapFrom(src => src.Amenities ?? Enumerable.Empty<Guid>()));

    CreateMap<HotelFeaturedDealsGetRequest, GetHotelFeaturedDealsQuery>();

    CreateMap<HotelCreationRequest, CreateHotelCommand>();

    CreateMap<HotelUpdateRequest, UpdateHotelCommand>();
  }
}