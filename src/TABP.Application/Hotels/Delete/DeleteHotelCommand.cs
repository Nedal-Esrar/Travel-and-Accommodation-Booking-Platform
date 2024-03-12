using MediatR;

namespace TABP.Application.Hotels.Delete;

public class DeleteHotelCommand : IRequest
{
  public Guid HotelId { get; init; }
}