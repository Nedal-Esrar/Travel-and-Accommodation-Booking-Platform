using FluentValidation;
using TABP.Api.Dtos.Bookings;

namespace TABP.Api.Validators.Bookings;

public class BookingCreationRequestValidator : AbstractValidator<BookingCreationRequest>
{
  public BookingCreationRequestValidator()
  {
    RuleFor(x => x.RoomIds)
      .NotEmpty();

    RuleFor(b => b.CheckInDateUtc)
      .NotEmpty()
      .GreaterThanOrEqualTo(DateOnly.FromDateTime(DateTime.UtcNow));

    RuleFor(b => b.CheckOutDateUtc)
      .NotEmpty()
      .GreaterThanOrEqualTo(b => b.CheckInDateUtc);

    RuleFor(x => x.PaymentMethod)
      .NotEmpty()
      .IsInEnum();
  }
}