using MediatR;

namespace TABP.Application.Hotels.GetForGuestById;

public record GetHotelForGuestByIdQuery(Guid HotelId) : IRequest<HotelForGuestResponse>;