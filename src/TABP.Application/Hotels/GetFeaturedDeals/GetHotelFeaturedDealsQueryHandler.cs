using AutoMapper;
using MediatR;
using TABP.Domain.Interfaces.Persistence.Repositories;

namespace TABP.Application.Hotels.GetFeaturedDeals;

public class
  GetHotelFeaturedDealsQueryHandler : IRequestHandler<GetHotelFeaturedDealsQuery,
  IEnumerable<HotelFeaturedDealResponse>>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;

  public GetHotelFeaturedDealsQueryHandler(IRoomClassRepository roomClassRepository, IMapper mapper)
  {
    _roomClassRepository = roomClassRepository;
    _mapper = mapper;
  }

  public async Task<IEnumerable<HotelFeaturedDealResponse>> Handle(GetHotelFeaturedDealsQuery request,
    CancellationToken cancellationToken)
  {
    ArgumentOutOfRangeException.ThrowIfNegativeOrZero(request.Count);
      
    var featuredDeals = await _roomClassRepository.GetFeaturedDealsInDifferentHotelsAsync(
      request.Count, cancellationToken);

    return _mapper.Map<IEnumerable<HotelFeaturedDealResponse>>(featuredDeals);
  }
}