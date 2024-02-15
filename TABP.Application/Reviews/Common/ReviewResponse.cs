namespace TABP.Application.Reviews.Common;

public record ReviewResponse(
  Guid Id,
  string Content,
  int Rating,
  DateTime CreatedAtUtc,
  DateTime? ModifiedAtUtc,
  string GuestName);