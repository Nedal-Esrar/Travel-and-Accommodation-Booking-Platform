using FluentValidation;
using TABP.Api.Dtos.Hotels;

namespace TABP.Api.Validators.Hotels;

public class RecentlyVisitedHotelsGetRequestValidator : AbstractValidator<RecentlyVisitedHotelsGetRequest>
{
  public RecentlyVisitedHotelsGetRequestValidator()
  {
    RuleFor(x => x.Count)
      .InclusiveBetween(1, 100);
  }
}