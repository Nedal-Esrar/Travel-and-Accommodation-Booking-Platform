using FluentValidation;

namespace TABP.Api.RateLimiting;

public class FixedWindowRateLimiterConfigValidator : AbstractValidator<FixedWindowRateLimiterConfig>
{
  public FixedWindowRateLimiterConfigValidator()
  {
    RuleFor(x => x.PermitLimit)
      .NotEmpty()
      .GreaterThan(0);

    RuleFor(x => x.TimeWindowSeconds)
      .NotEmpty()
      .GreaterThan(0);

    RuleFor(x => x.QueueProcessingOrder)
      .NotNull()
      .IsInEnum();

    RuleFor(x => x.QueueLimit)
      .NotEmpty()
      .GreaterThan(0);
  }
}