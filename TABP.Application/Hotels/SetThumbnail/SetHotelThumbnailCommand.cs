using MediatR;
using Microsoft.AspNetCore.Http;

namespace TABP.Application.Hotels.SetThumbnail;

public record SetHotelThumbnailCommand(
  Guid HotelId,
  IFormFile Image) : IRequest;