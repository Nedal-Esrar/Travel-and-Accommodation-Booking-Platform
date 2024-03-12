using FluentValidation;
using TABP.Api.Dtos.Cities;

namespace TABP.Api.Validators.Cities;

public class TrendingCitiesGetRequestValidator : AbstractValidator<TrendingCitiesGetRequest>
{
  public TrendingCitiesGetRequestValidator()
  {
    RuleFor(x => x.Count)
      .InclusiveBetween(1, 100);
  }
}