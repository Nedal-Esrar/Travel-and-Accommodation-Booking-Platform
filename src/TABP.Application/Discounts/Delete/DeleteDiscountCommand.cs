using MediatR;

namespace TABP.Application.Discounts.Delete;

public class DeleteDiscountCommand : IRequest
{
  public Guid RoomClassId { get; init; }
  public Guid DiscountId { get; init; }
}