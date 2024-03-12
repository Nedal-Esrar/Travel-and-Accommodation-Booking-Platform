using MediatR;
using Microsoft.AspNetCore.Http;

namespace TABP.Application.Hotels.SetThumbnail;

public class SetHotelThumbnailCommand : IRequest
{
  public Guid HotelId { get; init; }
  public IFormFile Image { get; init; }
}