using MediatR;

namespace TABP.Application.Hotels.GetFeaturedDeals;

public record GetHotelFeaturedDealsQuery(int Count) : IRequest<IEnumerable<HotelFeaturedDealResponse>>;