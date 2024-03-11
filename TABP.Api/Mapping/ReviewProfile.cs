using AutoMapper;
using TABP.Api.Dtos.Reviews;
using TABP.Application.Reviews.Create;
using TABP.Application.Reviews.GetByHotelId;
using TABP.Application.Reviews.Update;
using static TABP.Api.Mapping.MappingUtilities;

namespace TABP.Api.Mapping;

public class ReviewProfile : Profile
{
  public ReviewProfile()
  {
    CreateMap<ReviewsGetRequest, GetReviewsByHotelIdQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));

    CreateMap<ReviewCreationRequest, CreateReviewCommand>();

    CreateMap<ReviewUpdateRequest, UpdateReviewCommand>();
  }
}