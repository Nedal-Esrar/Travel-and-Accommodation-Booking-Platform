using FluentValidation;
using TABP.Api.Dtos.Reviews;
using static TABP.Domain.Constants.Common;
using static TABP.Domain.Constants.Review;

namespace TABP.Api.Validators.Reviews;

public class ReviewCreationRequestValidator : AbstractValidator<ReviewCreationRequest>
{
  public ReviewCreationRequestValidator()
  {
    RuleFor(x => x.Content)
      .NotEmpty()
      .MaximumLength(TextMaxLength);

    RuleFor(x => x.Rating)
      .NotEmpty()
      .InclusiveBetween(MinReviewRating, MaxReviewRating);
  }
}