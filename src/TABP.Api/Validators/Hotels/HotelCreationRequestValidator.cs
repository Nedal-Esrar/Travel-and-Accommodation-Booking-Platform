using FluentValidation;
using TABP.Api.Dtos.Hotels;
using TABP.Application.Extensions.Validation;
using static TABP.Domain.Constants.Common;
using static TABP.Domain.Constants.Hotel;
using static TABP.Domain.Constants.Location;

namespace TABP.Api.Validators.Hotels;

public class HotelCreationRequestValidator : AbstractValidator<HotelCreationRequest>
{
  public HotelCreationRequestValidator()
  {
    RuleFor(x => x.CityId)
      .NotEmpty();

    RuleFor(x => x.OwnerId)
      .NotEmpty();

    RuleFor(x => x.Name)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(x => x.StarRating)
      .NotEmpty()
      .InclusiveBetween(MinStarRating, MaxStarRating);

    RuleFor(x => x.Longitude)
      .NotEmpty()
      .InclusiveBetween(MinLongitude, MaxLongitude);

    RuleFor(x => x.Latitude)
      .NotEmpty()
      .InclusiveBetween(MinLatitude, MaxLatitude);

    RuleFor(x => x.BriefDescription)
      .MaximumLength(ShortTextMaxLength);

    RuleFor(x => x.Description)
      .MaximumLength(TextMaxLength);

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .PhoneNumber();
  }
}