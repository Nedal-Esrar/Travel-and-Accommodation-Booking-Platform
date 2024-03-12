using MediatR;
using TABP.Application.Reviews.Common;

namespace TABP.Application.Reviews.GetById;

public class GetReviewByIdQuery : IRequest<ReviewResponse>
{
  public Guid HotelId { get; init; }
  public Guid ReviewId { get; init; }
}