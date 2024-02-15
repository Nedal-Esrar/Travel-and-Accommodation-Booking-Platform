using AutoMapper;
using TABP.Application.Rooms.Create;
using TABP.Application.Rooms.GetByRoomClassIdForGuest;
using TABP.Application.Rooms.GetForManagement;
using TABP.Application.Rooms.Update;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Application.Mapping;

public class RoomProfile : Profile
{
  public RoomProfile()
  {
    CreateMap<PaginatedList<Room>, PaginatedList<RoomForGuestResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<PaginatedList<RoomForManagement>, PaginatedList<RoomForManagementResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<CreateRoomCommand, Room>();
    CreateMap<UpdateRoomCommand, Room>();
    CreateMap<Room, RoomForGuestResponse>();
    CreateMap<RoomForManagement, RoomForManagementResponse>();
  }
}