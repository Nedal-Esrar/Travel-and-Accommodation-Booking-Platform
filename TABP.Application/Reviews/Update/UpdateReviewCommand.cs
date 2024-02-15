using MediatR;

namespace TABP.Application.Reviews.Update;

public record UpdateReviewCommand(
  Guid ReviewId,
  Guid GuestId,
  Guid HotelId,
  string Content,
  int Rating) : IRequest;