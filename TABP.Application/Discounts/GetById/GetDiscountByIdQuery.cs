using MediatR;

namespace TABP.Application.Discounts.GetById;

public class GetDiscountByIdQuery : IRequest<DiscountResponse>
{
  public Guid RoomClassId { get; init; }
  public Guid DiscountId { get; init; }
}