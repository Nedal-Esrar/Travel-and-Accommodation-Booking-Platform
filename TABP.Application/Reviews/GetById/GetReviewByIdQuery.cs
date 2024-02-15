using MediatR;
using TABP.Application.Reviews.Common;

namespace TABP.Application.Reviews.GetById;

public record GetReviewByIdQuery(
  Guid HotelId,
  Guid ReviewId) : IRequest<ReviewResponse>;