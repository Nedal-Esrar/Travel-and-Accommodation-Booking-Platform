using Microsoft.AspNetCore.Http;
using TABP.Domain.Entities;

namespace TABP.Domain.Interfaces.Persistence.Services;

public interface IImageService
{
  Task<Image> StoreAsync(
    IFormFile image,
    CancellationToken cancellationToken = default);

  Task DeleteAsync(
    Image image,
    CancellationToken cancellationToken = default);
}