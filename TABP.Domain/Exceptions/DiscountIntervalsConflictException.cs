namespace TABP.Domain.Exceptions;

public class DiscountIntervalsConflictException(string message) : ConflictException(message)
{
  public override string Title => "Discounts activation date intervals conflict";
}