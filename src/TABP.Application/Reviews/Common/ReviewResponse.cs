namespace TABP.Application.Reviews.Common;

public class ReviewResponse
{
  public Guid Id { get; init; }
  public string Content { get; init; }
  public int Rating { get; init; }
  public DateTime CreatedAtUtc { get; init; }
  public DateTime? ModifiedAtUtc { get; init; }
  public string GuestName { get; init; }
}