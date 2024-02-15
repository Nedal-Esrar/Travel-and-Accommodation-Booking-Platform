namespace TABP.Domain.Exceptions;

public class HotelLocationReplicationException(string message) : ConflictException(message)
{
  public override string Title => "Another hotel in the same location exists";
}