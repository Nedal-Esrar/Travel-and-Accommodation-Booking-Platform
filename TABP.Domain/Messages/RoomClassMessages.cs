namespace TABP.Domain.Messages;

public static class RoomClassMessages
{
  public const string NotFound = "Room class with the given ID is not found.";
  public const string NameInHotelFound = "A room class with the same name already exists in the specified hotel.";
  public const string DuplicatedRoomNumber = "A room with the same number already exists in the specified room class.";
  public const string RoomNotFound = "The specified room does not exist in the specified room class.";
  public const string DependentsExist = "There are existing rooms for the specified room class.";
}