using MediatR;
using Microsoft.AspNetCore.Http;

namespace TABP.Application.Hotels.AddToGallery;

public class AddImageToHotelGalleryCommand : IRequest
{
  public Guid HotelId { get; init; }
  public IFormFile Image { get; init; }
}