using MediatR;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Rooms.GetForManagement;

public record GetRoomsForManagementQuery(
  Guid RoomClassId,
  string? SearchTerm,
  SortOrder? SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize) : IRequest<PaginatedList<RoomForManagementResponse>>;