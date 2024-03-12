namespace TABP.Domain.Messages;

public static class BookingMessages
{
  public const string NotFound = "Booking with the provided Id is not found";
  public const string CheckedIn = "Can't cancel the booking with the provided ID because the check-in date has passed.";
  public const string NotFoundForGuest = "Booking with the provided ID is not found for the specified guest.";

  public const string NoBookingForGuestInHotel =
    "The specified guest did not make any bookings in the specified hotel.";
}