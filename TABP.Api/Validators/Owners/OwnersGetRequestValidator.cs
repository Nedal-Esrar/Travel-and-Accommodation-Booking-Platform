using FluentValidation;
using TABP.Api.Dtos.Owners;
using TABP.Api.Validators.Common;

namespace TABP.Api.Validators.Owners;

public class OwnersGetRequestValidator : AbstractValidator<OwnersGetRequest>
{
  public OwnersGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(OwnerValidationMessages.SortColumnNotValid);
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
      "FirstName",
      "LastName"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}