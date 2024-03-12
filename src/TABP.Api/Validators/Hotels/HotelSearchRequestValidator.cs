using FluentValidation;
using TABP.Api.Dtos.Hotels;
using TABP.Api.Validators.Common;
using static TABP.Domain.Constants.RoomClass;
using static TABP.Domain.Constants.Common;
using static TABP.Domain.Constants.Hotel;

namespace TABP.Api.Validators.Hotels;

public class HotelSearchRequestValidator : AbstractValidator<HotelSearchRequest>
{
  public HotelSearchRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
    
    RuleFor(x => x.SearchTerm)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.CheckInDateUtc)
      .NotEmpty()
      .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

    RuleFor(x => x.CheckOutDateUtc)
      .NotEmpty()
      .GreaterThanOrEqualTo(x => x.CheckInDateUtc);

    RuleFor(x => x.NumberOfAdults)
      .NotEmpty()
      .GreaterThanOrEqualTo(MinAdultsCapacity);

    RuleFor(x => x.NumberOfChildren)
      .NotNull()
      .GreaterThanOrEqualTo(MinChildrenCapacity);

    RuleFor(x => x.NumberOfRooms)
      .NotEmpty()
      .GreaterThan(Zero);

    When(x => x.MinStarRating is not null, () =>
    {
      RuleFor(x => x.MinStarRating)
        .InclusiveBetween(MinStarRating, MaxStarRating);
    });

    RuleFor(x => x.MinPrice)
      .GreaterThan(Zero);

    RuleFor(x => x.MaxPrice)
      .GreaterThan(x => x.MinPrice ?? Zero);

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