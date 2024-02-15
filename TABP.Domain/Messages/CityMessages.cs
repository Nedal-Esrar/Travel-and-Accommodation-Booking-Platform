namespace TABP.Domain.Messages;

public static class CityMessages
{
  public const string NotFound = "City with the given ID is not found.";
  public const string PostOfficeExists = "A city with the provided postal code already exists.";
  public const string DependentsExist = "The specified city still has dependents attached to it.";
}