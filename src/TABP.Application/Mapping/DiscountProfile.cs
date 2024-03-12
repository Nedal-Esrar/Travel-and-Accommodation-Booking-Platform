using AutoMapper;
using TABP.Application.Discounts.Create;
using TABP.Application.Discounts.GetById;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Application.Mapping;

public class DiscountProfile : Profile
{
  public DiscountProfile()
  {
    CreateMap<PaginatedList<Discount>, PaginatedList<Discount>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
    CreateMap<Discount, DiscountResponse>();
    CreateMap<CreateDiscountCommand, Discount>();
  }
}