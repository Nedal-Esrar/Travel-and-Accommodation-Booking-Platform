using FluentValidation;
using TABP.Api.Dtos.Cities;
using TABP.Api.Validators.Common;
using static TABP.Domain.Constants.Common;

namespace TABP.Api.Validators.Cities;

public class CitiesGetRequestValidator : AbstractValidator<CitiesGetRequest>
{
  public CitiesGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SearchTerm)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(CitiesValidationMessages.ShouldBeAValidSortColumn);
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
      "Name",
      "Country",
      "PostOffice"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}