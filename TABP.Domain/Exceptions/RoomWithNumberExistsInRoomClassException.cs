namespace TABP.Domain.Exceptions;

public class RoomWithNumberExistsInRoomClassException(string message) : ConflictException(message)
{
  public override string Title => "A room with the same number exists in the room class";
}