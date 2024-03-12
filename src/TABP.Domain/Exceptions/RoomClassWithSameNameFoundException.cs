namespace TABP.Domain.Exceptions;

public class RoomClassWithSameNameFoundException(string message) : ConflictException(message)
{
  public override string Title => "Another room class in the same hotel exists";
}