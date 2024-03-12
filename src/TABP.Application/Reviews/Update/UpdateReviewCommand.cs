using MediatR;

namespace TABP.Application.Reviews.Update;

public class UpdateReviewCommand : IRequest
{
  public Guid ReviewId { get; init; }
  public Guid GuestId { get; init; }
  public Guid HotelId { get; init; }
  public string Content { get; init; }
  public int Rating { get; init; }
}