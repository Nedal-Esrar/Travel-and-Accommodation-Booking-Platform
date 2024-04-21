using System.Threading.RateLimiting;

namespace TABP.Api.RateLimiting;

public class FixedWindowRateLimiterConfig
{
  public required int PermitLimit { get; set; }
  public required double TimeWindowSeconds { get; set; }
  public required QueueProcessingOrder QueueProcessingOrder { get; set; }
  public required int QueueLimit { get; set; }
}