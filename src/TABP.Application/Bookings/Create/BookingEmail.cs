using TABP.Domain.Models;

namespace TABP.Application.Bookings.Create;

public static class BookingEmail
{
  public static EmailRequest GetBookingEmailRequest(string toEmail,
    IEnumerable<(string name, byte[] file)> attachments)
  {
    return new EmailRequest(
      [toEmail],
      "Reservation is Confirmed!",
      "Thank you for reserving on our platform, here is the invoice",
      attachments);
  }
}