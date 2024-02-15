using MediatR;

namespace TABP.Application.Discounts.Delete;

public record DeleteDiscountCommand(Guid RoomClassId, Guid DiscountId) : IRequest;