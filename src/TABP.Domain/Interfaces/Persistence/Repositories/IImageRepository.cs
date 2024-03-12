using Microsoft.AspNetCore.Http;
using TABP.Domain.Entities;
using TABP.Domain.Enums;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IImageRepository
{
  Task<Image> CreateAsync(IFormFile image, Guid entityId, ImageType type,
    CancellationToken cancellationToken = default);

  Task DeleteForAsync(Guid entityId, ImageType type, CancellationToken cancellationToken = default);
}