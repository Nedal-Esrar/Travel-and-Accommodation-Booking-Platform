using MediatR;

namespace TABP.Application.Hotels.GetForGuestById;

public class GetHotelForGuestByIdQuery : IRequest<HotelForGuestResponse>
{
  public Guid HotelId { get; init; }
}