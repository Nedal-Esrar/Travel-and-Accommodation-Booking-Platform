using AutoMapper;
using TABP.Api.Dtos.Rooms;
using TABP.Application.Rooms.Create;
using TABP.Application.Rooms.GetByRoomClassIdForGuest;
using TABP.Application.Rooms.GetForManagement;
using TABP.Application.Rooms.Update;
using static TABP.Api.Mapping.MappingUtilities;

namespace TABP.Api.Mapping;

public class RoomProfile : Profile
{
  public RoomProfile()
  {
    CreateMap<RoomsGetRequest, GetRoomsForManagementQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<RoomsForGuestsGetRequest, GetRoomsByRoomClassIdForGuestsQuery>();

    CreateMap<RoomCreationRequest, CreateRoomCommand>();

    CreateMap<RoomUpdateRequest, UpdateRoomCommand>();
  }
}