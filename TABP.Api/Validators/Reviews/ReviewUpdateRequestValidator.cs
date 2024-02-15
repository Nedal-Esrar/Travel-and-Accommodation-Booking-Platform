using FluentValidation;
using TABP.Api.Dtos.Reviews;

namespace TABP.Api.Validators.Reviews;

public class ReviewUpdateRequestValidator : AbstractValidator<ReviewUpdateRequest>
{
  public ReviewUpdateRequestValidator()
  {
    RuleFor(x => x.Content)
      .NotEmpty()
      .MaximumLength(200);

    RuleFor(x => x.Rating)
      .NotEmpty()
      .InclusiveBetween(0, 10);
  }
}