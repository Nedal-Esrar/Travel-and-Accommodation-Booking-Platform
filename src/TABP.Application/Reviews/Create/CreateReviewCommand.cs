using MediatR;
using TABP.Application.Reviews.Common;

namespace TABP.Application.Reviews.Create;

public class CreateReviewCommand : IRequest<ReviewResponse>
{
  public Guid GuestId { get; init; }
  public Guid HotelId { get; init; }
  public string Content { get; init; }
  public int Rating { get; init; }
}