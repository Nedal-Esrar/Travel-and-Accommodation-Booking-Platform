using AutoMapper;
using TABP.Application.Amenities.Common;
using TABP.Application.Amenities.Create;
using TABP.Application.Amenities.Update;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Application.Mapping;

public class AmenityProfile : Profile
{
  public AmenityProfile()
  {
    CreateMap<PaginatedList<Amenity>, PaginatedList<AmenityResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<Amenity, AmenityResponse>();
    CreateMap<UpdateAmenityCommand, Amenity>();
    CreateMap<CreateAmenityCommand, Amenity>();
  }
}