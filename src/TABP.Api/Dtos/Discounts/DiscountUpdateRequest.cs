using MediatR;

namespace TABP.Api.Dtos.Discounts;

public class DiscountUpdateRequest : IRequest
{
  public decimal Percentage { get; init; }
  public DateTime StartDateUtc { get; init; }
  public DateTime EndDateUtc { get; init; }
}