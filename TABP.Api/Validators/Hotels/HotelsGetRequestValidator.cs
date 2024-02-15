using FluentValidation;
using TABP.Api.Dtos.Hotels;
using TABP.Api.Validators.Common;

namespace TABP.Api.Validators.Hotels;

public class HotelsGetRequestValidator : AbstractValidator<HotelsGetRequest>
{
  public HotelsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(HotelValidationMessages.GetSortColumnNotValid);
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