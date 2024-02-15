using MediatR;
using TABP.Application.Reviews.Common;

namespace TABP.Application.Reviews.Create;

public record CreateReviewCommand(
  Guid GuestId,
  Guid HotelId,
  string Content,
  int Rating) : IRequest<ReviewResponse>;