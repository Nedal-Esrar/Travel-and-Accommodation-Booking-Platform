using FluentValidation;
using TABP.Api.Dtos.Hotels;
using TABP.Application.Extensions.Validation;

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
      .ValidName(3, 30);

    RuleFor(x => x.StarRating)
      .NotEmpty()
      .InclusiveBetween(1, 5);

    RuleFor(x => x.Longitude)
      .NotEmpty()
      .InclusiveBetween(-90, 90);

    RuleFor(x => x.Latitude)
      .NotEmpty()
      .InclusiveBetween(-180, 180);

    RuleFor(x => x.BriefDescription)
      .MaximumLength(100);

    RuleFor(x => x.Description)
      .MaximumLength(100);

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .PhoneNumber();
  }
}