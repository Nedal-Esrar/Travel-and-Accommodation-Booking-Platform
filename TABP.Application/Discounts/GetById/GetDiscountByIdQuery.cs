using MediatR;

namespace TABP.Application.Discounts.GetById;

public record GetDiscountByIdQuery(
  Guid RoomClassId,
  Guid DiscountId) : IRequest<DiscountResponse>;