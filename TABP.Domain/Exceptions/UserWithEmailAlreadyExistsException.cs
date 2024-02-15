namespace TABP.Domain.Exceptions;

public class UserWithEmailAlreadyExistsException(string message) : ConflictException(message)
{
  public override string Title => "A user with the same email exists";
}