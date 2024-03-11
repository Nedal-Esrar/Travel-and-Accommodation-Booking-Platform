using FluentValidation;
using TABP.Api.Dtos.Amenities;
using TABP.Api.Validators.Common;
using static TABP.Domain.Constants.Common;

namespace TABP.Api.Validators.Amenities;

public class AmenitiesGetRequestValidator : AbstractValidator<AmenitiesGetRequest>
{
  public AmenitiesGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SearchTerm)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(AmenitiesValidationMessages.ShouldBeAValidSortColumn);
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
      "Name"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}