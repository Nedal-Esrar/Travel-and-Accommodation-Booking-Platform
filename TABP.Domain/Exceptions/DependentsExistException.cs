namespace TABP.Domain.Exceptions;

public class DependentsExistException(string message) : ConflictException(message)
{
  public override string Title => "Dependents on the resource exists";
}