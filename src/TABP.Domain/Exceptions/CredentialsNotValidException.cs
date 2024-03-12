namespace TABP.Domain.Exceptions;

public class CredentialsNotValidException(string message) : UnauthorizedException(message)
{
  public override string Title => "Provided Credentials are not valid";
}