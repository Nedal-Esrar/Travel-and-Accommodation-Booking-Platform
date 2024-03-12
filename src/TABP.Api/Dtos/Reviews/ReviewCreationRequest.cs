namespace TABP.Api.Dtos.Reviews;

public class ReviewCreationRequest
{
  public string Content { get; init; }
  public int Rating { get; init; }
}