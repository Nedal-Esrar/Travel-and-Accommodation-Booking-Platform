using MediatR;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.RoomClasses.GetForManagement;

public record GetRoomClassesForManagementQuery(
  string? SearchTerm,
  SortOrder? SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize) : IRequest<PaginatedList<RoomClassForManagementResponse>>;