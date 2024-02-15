namespace TABP.Domain.Messages;

public static class DiscountMessages
{
  public const string NotFound = "Discount with the given ID is not found.";
  public const string NotFoundInRoomClass = "Discount with the given ID is not found in the specified room class.";
  public const string InDateIntervalExists = "Another discount already exists within the same date interval.";
}