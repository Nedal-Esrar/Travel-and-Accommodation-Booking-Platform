using FluentValidation;
using TABP.Api.Dtos.Discounts;

namespace TABP.Api.Validators.Discounts;

public class DiscountCreationRequestValidator : AbstractValidator<DiscountCreationRequest>
{
  public DiscountCreationRequestValidator()
  {
    RuleFor(x => x.Percentage)
      .NotEmpty()
      .GreaterThan(0)
      .LessThanOrEqualTo(100);

    RuleFor(x => x.StartDateUtc)
      .NotEmpty()
      .GreaterThan(DateTime.UtcNow);

    RuleFor(x => x.EndDateUtc)
      .NotEmpty()
      .GreaterThan(x => x.StartDateUtc);
  }
}