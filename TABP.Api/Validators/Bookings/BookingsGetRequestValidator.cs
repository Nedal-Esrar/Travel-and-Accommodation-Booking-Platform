using FluentValidation;
using TABP.Api.Dtos.Bookings;
using TABP.Api.Validators.Common;

namespace TABP.Api.Validators.Bookings;

public class BookingsGetRequestValidator : AbstractValidator<BookingsGetRequest>
{
  public BookingsGetRequestValidator()
  {
    Include(new ResourcesQueryRequestValidator());
  }
}