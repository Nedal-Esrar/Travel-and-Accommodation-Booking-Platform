using MediatR;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Hotels.GetForManagement;

public class GetHotelsForManagementQuery : IRequest<PaginatedList<HotelForManagementResponse>>
{
  public string? SearchTerm { get; init; }
  public SortOrder? SortOrder { get; init; }
  public string? SortColumn { get; init; }
  public int PageNumber { get; init; }
  public int PageSize { get; init; }
}