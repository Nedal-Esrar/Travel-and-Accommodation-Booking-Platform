using AutoMapper;
using MediatR;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;

namespace TABP.Application.Hotels.GetForGuestById;

public class GetHotelForGuestByIdQueryHandler : IRequestHandler<GetHotelForGuestByIdQuery, HotelForGuestResponse>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;

  public GetHotelForGuestByIdQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _mapper = mapper;
  }

  public async Task<HotelForGuestResponse> Handle(GetHotelForGuestByIdQuery request,
    CancellationToken cancellationToken)
  {
    var hotel = await _hotelRepository.GetByIdAsync(
                  request.HotelId,
                  true,
                  true,
                  true,
                  cancellationToken)
                ?? throw new NotFoundException(HotelMessages.NotFound);

    return _mapper.Map<HotelForGuestResponse>(hotel);
  }
}