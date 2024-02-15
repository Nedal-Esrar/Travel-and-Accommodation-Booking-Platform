using MediatR;

namespace TABP.Api.Dtos.Discounts;

public record DiscountUpdateRequest(
  decimal Percentage,
  DateTime StartDateUtc,
  DateTime EndDateUtc) : IRequest;