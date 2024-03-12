using AutoMapper;
using TABP.Application.Owners.Common;
using TABP.Application.Owners.Create;
using TABP.Application.Owners.Update;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Application.Mapping;

public class OwnerProfile : Profile
{
  public OwnerProfile()
  {
    CreateMap<PaginatedList<Owner>, PaginatedList<OwnerResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<Owner, OwnerResponse>();
    CreateMap<CreateOwnerCommand, Owner>();
    CreateMap<UpdateOwnerCommand, Owner>();
  }
}