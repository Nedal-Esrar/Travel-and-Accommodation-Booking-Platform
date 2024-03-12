using FluentValidation;
using TABP.Api.Dtos.Hotels;

namespace TABP.Api.Validators.Hotels;

public class HotelFeaturedDealsGetRequestValidator : AbstractValidator<HotelFeaturedDealsGetRequest>
{
  public HotelFeaturedDealsGetRequestValidator()
  {
    RuleFor(x => x.Count)
      .InclusiveBetween(1, 100);
  }
}