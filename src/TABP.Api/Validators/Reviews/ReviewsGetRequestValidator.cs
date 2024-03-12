using FluentValidation;
using TABP.Api.Dtos.Reviews;
using TABP.Api.Validators.Common;

namespace TABP.Api.Validators.Reviews;

public class ReviewsGetRequestValidator : AbstractValidator<ReviewsGetRequest>
{
  public ReviewsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(ReviewValidationMessages.SortColumnNotValid);
  }

  private static bool BeAValidSortColumn(string? sortColumn)
  {
    if (string.IsNullOrEmpty(sortColumn))
    {
      return true;
    }

    var validColumns = new[]
    {
      "id",
      "rating"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}