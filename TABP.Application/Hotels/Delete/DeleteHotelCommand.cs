using MediatR;

namespace TABP.Application.Hotels.Delete;

public record DeleteHotelCommand(Guid HotelId) : IRequest;