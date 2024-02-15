namespace TABP.Domain.Messages;

public static class HotelMessages
{
  public const string NotFound = "Hotel with the given ID is not found.";
  public const string SameLocationExists = "A hotel in the same location (longitude, latitude) already exists.";
  public const string DependentsExist = "The specified hotel still has dependents attached to it.";
}