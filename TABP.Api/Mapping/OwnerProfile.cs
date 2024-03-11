using AutoMapper;
using TABP.Api.Dtos.Owners;
using TABP.Application.Owners.Create;
using TABP.Application.Owners.Get;
using TABP.Application.Owners.Update;
using static TABP.Api.Mapping.MappingUtilities;

namespace TABP.Api.Mapping;

public class OwnerProfile : Profile
{
  public OwnerProfile()
  {
    CreateMap<OwnersGetRequest, GetOwnersQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<OwnerCreationRequest, CreateOwnerCommand>();
    
    CreateMap<OwnerUpdateRequest, UpdateOwnerCommand>();
  }
}