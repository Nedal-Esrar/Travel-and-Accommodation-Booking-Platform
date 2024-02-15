using FluentValidation;
using TABP.Api.Dtos.Reviews;

namespace TABP.Api.Validators.Reviews;

public class ReviewCreationRequestValidator : AbstractValidator<ReviewCreationRequest>
{
  public ReviewCreationRequestValidator()
  {
    RuleFor(x => x.Content)
      .NotEmpty()
      .MaximumLength(200);

    RuleFor(x => x.Rating)
      .NotEmpty()
      .InclusiveBetween(0, 10);
  }
}