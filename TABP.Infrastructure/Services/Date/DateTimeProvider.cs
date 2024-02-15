using TABP.Domain.Interfaces.Services;

namespace TABP.Infrastructure.Services.Date;

public class DateTimeProvider : IDateTimeProvider
{
  public DateTime GetCurrentDateTimeUtc()
  {
    return DateTime.UtcNow;
  }

  public DateOnly GetCurrentDateUtc()
  {
    return DateOnly.FromDateTime(DateTime.UtcNow);
  }
}