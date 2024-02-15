using MediatR;
using Microsoft.AspNetCore.Http;

namespace TABP.Application.Cities.SetThumbnail;

public record SetCityThumbnailCommand(
  Guid CityId,
  IFormFile Image) : IRequest;