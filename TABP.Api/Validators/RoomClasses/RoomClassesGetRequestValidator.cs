using FluentValidation;
using TABP.Api.Dtos.RoomClasses;
using TABP.Api.Validators.Common;

namespace TABP.Api.Validators.RoomClasses;

public class RoomClassesGetRequestValidator : AbstractValidator<RoomClassesGetRequest>
{
  public RoomClassesGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(RoomClassesValidationMessages.SortColumnNotValid);
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
      "AdultsCapacity",
      "ChildrenCapacity",
      "PricePerNight"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}