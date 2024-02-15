using MediatR;
using Microsoft.AspNetCore.Http;

namespace TABP.Application.Hotels.AddToGallery;

public record AddImageToHotelGalleryCommand(
  Guid HotelId,
  IFormFile Image) : IRequest;