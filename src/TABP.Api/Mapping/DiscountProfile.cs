using AutoMapper;
using TABP.Api.Dtos.Discounts;
using TABP.Application.Discounts.Create;
using TABP.Application.Discounts.Get;
using static TABP.Api.Mapping.MappingUtilities;

namespace TABP.Api.Mapping;

public class DiscountProfile : Profile
{
  public DiscountProfile()
  {
    CreateMap<DiscountsGetRequest, GetDiscountsQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<DiscountCreationRequest, CreateDiscountCommand>();
  }
}