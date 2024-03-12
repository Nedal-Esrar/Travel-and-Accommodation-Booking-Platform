using AutoMapper;
using TABP.Api.Dtos.RoomClasses;
using TABP.Application.RoomClasses.Create;
using TABP.Application.RoomClasses.GetByHotelIdForGuest;
using TABP.Application.RoomClasses.GetForManagement;
using TABP.Application.RoomClasses.Update;
using static TABP.Api.Mapping.MappingUtilities;

namespace TABP.Api.Mapping;

public class RoomClassProfile : Profile
{
  public RoomClassProfile()
  {
    CreateMap<GetRoomClassesForGuestRequest, GetRoomClassesByHotelIdForGuestQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));
    
    CreateMap<RoomClassesGetRequest, GetRoomClassesForManagementQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<RoomClassCreationRequest, CreateRoomClassCommand>()
      .ForMember(dst => dst.AmenitiesIds, opt => opt.MapFrom(src => src.AmenitiesIds ?? Enumerable.Empty<Guid>()));

    CreateMap<RoomClassUpdateRequest, UpdateRoomClassCommand>();
  }
}