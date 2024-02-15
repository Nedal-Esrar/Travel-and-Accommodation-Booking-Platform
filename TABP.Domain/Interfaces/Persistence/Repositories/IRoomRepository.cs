using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Domain.Interfaces.Persistence.Repositories;

public interface IRoomRepository
{
  Task<PaginatedList<RoomForManagement>> GetForManagementAsync(
    PaginationQuery<Room> query,
    CancellationToken cancellationToken = default);

  Task<Room?> GetByIdAsync(Guid roomClassId, Guid id, CancellationToken cancellationToken = default);

  Task<Room> CreateAsync(Room room, CancellationToken cancellationToken = default);

  Task UpdateAsync(Room room, CancellationToken cancellationToken = default);

  Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
  Task<bool> ExistsByRoomClassIdAsync(Guid roomClassId, CancellationToken cancellationToken = default);
  Task<bool> ExistsByIdAndRoomClassIdAsync(Guid roomClassId, Guid id, CancellationToken cancellationToken = default);

  Task<bool> ExistsByNumberInRoomClassAsync(string number, Guid roomClassId,
    CancellationToken cancellationToken = default);

  Task<PaginatedList<Room>> GetAsync(PaginationQuery<Room> query, CancellationToken cancellationToken = default);
  Task<Room?> GetByIdWithRoomClassAsync(Guid roomId, CancellationToken cancellationToken = default);

  Task<bool> IsAvailableAsync(Guid roomId, DateOnly checkInDate, DateOnly checkOutDate,
    CancellationToken cancellationToken = default);
}