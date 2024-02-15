using MediatR;
using TABP.Application.Discounts.GetById;

namespace TABP.Application.Discounts.Create;

public record CreateDiscountCommand(
  Guid RoomClassId,
  decimal Percentage,
  DateTime StartDateUtc,
  DateTime EndDateUtc) : IRequest<DiscountResponse>;