using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IHotelRepository
{
  Task<PaginatedList<HotelForManagement>> GetForManagementAsync(PaginationQuery<Hotel> query,
    CancellationToken cancellationToken = default);

  Task<Hotel?> GetByIdAsync(Guid id, bool includeCity = false, bool includeThumbnail = false,
    bool includeGallery = false, CancellationToken cancellationToken = default);

  Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken = default);

  Task<Hotel> CreateAsync(Hotel hotel, CancellationToken cancellationToken = default);

  Task UpdateAsync(Hotel hotel, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
  Task<bool> ExistsByCityIdAsync(Guid cityId, CancellationToken cancellationToken);
  Task<bool> ExistsByLocation(double longitude, double latitude);

  Task<PaginatedList<HotelSearchResult>> GetForSearchAsync(PaginationQuery<Hotel> query,
    CancellationToken cancellationToken = default);

  Task UpdateReviewById(Guid id, double newRating, CancellationToken cancellationToken = default);
}