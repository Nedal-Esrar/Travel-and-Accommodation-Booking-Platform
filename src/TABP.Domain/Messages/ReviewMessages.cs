namespace TABP.Domain.Messages;

public static class ReviewMessages
{
  public const string NotFound = "The review with the given ID is not found.";
  public const string WithIdNotFoundInHotelWithId =
    "Review with the specified ID is not found for the hotel with the specified ID.";

  public const string NotFoundForUserForHotel = "The specified review is not found for the specified user and hotel.";
  public const string GuestAlreadyReviewedHotel = "The specified guest has already reviewed the specified hotel.";
}