namespace TABP.Domain.Messages;

public static class RoomMessages
{
  public const string NotFound = "Room with the given ID is not found.";
  public const string NotInSameHotel = "The provided rooms are not in the same hotel.";
  public const string DependentsExist = "There are existing bookings for the specified room.";

  public static string NotAvailable(Guid roomId)
  {
    return $"Room with ID {roomId} is not available during the specified time interval.";
  }
}