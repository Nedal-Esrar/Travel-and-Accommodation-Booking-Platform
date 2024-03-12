using AutoMapper;
using TABP.Api.Dtos.Amenities;
using TABP.Application.Amenities.Create;
using TABP.Application.Amenities.Get;
using TABP.Application.Amenities.Update;
using static TABP.Api.Mapping.MappingUtilities;

namespace TABP.Api.Mapping;

public class AmenityProfile : Profile
{
  public AmenityProfile()
  {
    CreateMap<AmenitiesGetRequest, GetAmenitiesQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<AmenityCreationRequest, CreateAmenityCommand>();

    CreateMap<AmenityUpdateRequest, UpdateAmenityCommand>();
  }
}