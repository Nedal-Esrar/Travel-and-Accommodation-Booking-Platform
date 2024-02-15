namespace TABP.Domain.Exceptions;

public class AmenityExistsException(string message) : ConflictException(message)
{
  public override string Title => "Amenity already exists";
}