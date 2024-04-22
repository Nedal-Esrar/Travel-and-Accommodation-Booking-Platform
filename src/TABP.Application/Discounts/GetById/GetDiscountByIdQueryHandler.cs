using AutoMapper;
using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Discounts.GetById;

public class GetDiscountByIdQueryHandler : IRequestHandler<GetDiscountByIdQuery, DiscountResponse>
{
  private readonly IDiscountRepository _discountRepository;
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;

  public GetDiscountByIdQueryHandler(
    IRoomClassRepository roomClassRepository,
    IDiscountRepository discountRepository,
    IMapper mapper)
  {
    _roomClassRepository = roomClassRepository;
    _discountRepository = discountRepository;
    _mapper = mapper;
  }

  public async Task<DiscountResponse> Handle(
    GetDiscountByIdQuery request,
    CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsAsync(rc => rc.Id == request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }

    var discount = await _discountRepository.GetByIdAsync(
      request.RoomClassId,
      request.DiscountId,
      cancellationToken);

    if (discount is null)
    {
      throw new NotFoundException(DiscountMessages.NotFoundInRoomClass);
    }

    return _mapper.Map<DiscountResponse>(discount);
  }
}