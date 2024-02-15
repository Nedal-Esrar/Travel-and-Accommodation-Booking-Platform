using FluentValidation;
using TABP.Api.Dtos.Hotels;
using TABP.Api.Validators.Common;

namespace TABP.Api.Validators.Hotels;

public class HotelSearchRequestValidator : AbstractValidator<HotelSearchRequest>
{
  public HotelSearchRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());

    RuleFor(x => x.CheckInDateUtc)
      .NotEmpty()
      .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

    RuleFor(x => x.CheckOutDateUtc)
      .NotEmpty()
      .GreaterThanOrEqualTo(x => x.CheckInDateUtc);

    RuleFor(x => x.NumberOfAdults)
      .NotEmpty()
      .GreaterThanOrEqualTo(1);

    RuleFor(x => x.NumberOfChildren)
      .NotEmpty()
      .GreaterThanOrEqualTo(0);

    RuleFor(x => x.NumberOfRooms)
      .NotEmpty()
      .GreaterThanOrEqualTo(1);

    When(x => x.MinStarRating is not null, () =>
    {
      RuleFor(x => x.MinStarRating)
        .InclusiveBetween(1, 5);
    });

    RuleFor(x => x.MinPrice)
      .GreaterThan(0);

    RuleFor(x => x.MaxPrice)
      .GreaterThan(x => x.MinPrice ?? 0);

    When(x => x.RoomTypes is not null, () =>
    {
      RuleForEach(x => x.RoomTypes)
        .IsInEnum()
        .WithMessage(HotelValidationMessages.RoomTypeNotValid);
    });

    RuleFor(x => x.SortColumn)
      .Must(BeAValidSortColumn)
      .WithMessage(HotelValidationMessages.SearchSortColumnNotValid);
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
      "name",
      "starRating",
      "price",
      "reviewsRating"
    };

    return Array.Exists(validColumns,
      col => string.Equals(col, sortColumn, StringComparison.OrdinalIgnoreCase));
  }
}