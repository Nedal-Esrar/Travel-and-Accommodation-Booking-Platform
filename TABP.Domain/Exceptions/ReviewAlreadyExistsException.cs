namespace TABP.Domain.Exceptions;

public class ReviewAlreadyExistsException(string message) : ConflictException(message)
{
  public override string Title => "Hotel reviewed already";
}