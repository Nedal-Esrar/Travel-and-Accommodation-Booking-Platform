using AutoMapper;
using TABP.Application.Reviews.Common;
using TABP.Application.Reviews.Create;
using TABP.Application.Reviews.Update;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Application.Mapping;

public class ReviewProfile : Profile
{
  public ReviewProfile()
  {
    CreateMap<CreateReviewCommand, Review>();
    CreateMap<UpdateReviewCommand, Review>();
    CreateMap<Review, ReviewResponse>();
    CreateMap<PaginatedList<Review>, PaginatedList<ReviewResponse>>()
      .ForMember(dst => dst.Items, options => options.MapFrom(src => src.Items));
  }
}