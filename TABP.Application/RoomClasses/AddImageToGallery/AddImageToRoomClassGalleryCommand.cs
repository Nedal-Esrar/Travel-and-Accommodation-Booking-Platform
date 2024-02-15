using MediatR;
using Microsoft.AspNetCore.Http;

namespace TABP.Application.RoomClasses.AddImageToGallery;

public record AddImageToRoomClassGalleryCommand(
  Guid RoomClassId,
  IFormFile Image) : IRequest;