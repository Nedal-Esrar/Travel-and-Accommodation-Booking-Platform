using FluentValidation;
using TABP.Api.Dtos.Rooms;
using TABP.Api.Validators.Common;

namespace TABP.Api.Validators.Rooms;

public class RoomsGetRequestValidator : AbstractValidator<RoomsGetRequest>
{
  public RoomsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(RoomValidationMessages.SortColumnNotValid);
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
      "number"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}