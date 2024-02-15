using MediatR;

namespace TABP.Application.Reviews.Delete;

public record DeleteReviewCommand(
  Guid GuestId,
  Guid HotelId,
  Guid ReviewId) : IRequest;