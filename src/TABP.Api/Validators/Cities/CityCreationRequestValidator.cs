using FluentValidation;
using TABP.Api.Dtos.Cities;
using TABP.Application.Extensions.Validation;
using static TABP.Domain.Constants.Common;
using static TABP.Domain.Constants.City;

namespace TABP.Api.Validators.Cities;

public class CityCreationRequestValidator : AbstractValidator<CityCreationRequest>
{
  public CityCreationRequestValidator()
  {
    RuleFor(c => c.Name)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(c => c.Country)
      .NotEmpty()
      .ValidName(MinNameLength, MaxNameLength);

    RuleFor(c => c.PostOffice)
      .NotEmpty()
      .ValidNumericString(PostOfficeLength);
  }
}