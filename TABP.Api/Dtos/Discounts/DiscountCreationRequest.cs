namespace TABP.Api.Dtos.Discounts;

public record DiscountCreationRequest(
  decimal Percentage,
  DateTime StartDateUtc,
  DateTime EndDateUtc);