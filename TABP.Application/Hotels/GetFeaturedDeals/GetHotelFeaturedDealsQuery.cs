using MediatR;

namespace TABP.Application.Hotels.GetFeaturedDeals;

public class GetHotelFeaturedDealsQuery : IRequest<IEnumerable<HotelFeaturedDealResponse>>
{
  public int Count { get; init; }
}