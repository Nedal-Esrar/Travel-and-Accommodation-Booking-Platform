namespace TABP.Domain.Exceptions;

public class UnauthorizedException(string message) : CustomException(message)
{
  public override string Title => "Unauthorized";
}