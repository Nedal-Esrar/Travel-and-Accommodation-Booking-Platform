namespace TABP.Domain.Exceptions;

public class ForbiddenException(string message) : CustomException(message)
{
  public override string Title => "Forbidden";
}