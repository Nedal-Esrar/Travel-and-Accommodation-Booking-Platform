namespace TABP.Domain.Exceptions;

public class GuestDidNotBookHotelException(string message) : ConflictException(message)
{
  public override string Title => "Guest did not book any room in the hotel";
}