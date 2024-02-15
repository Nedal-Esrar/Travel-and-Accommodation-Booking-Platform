using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Models;

namespace TABP.Application.Hotels.GetForManagement;

public class GetHotelsForManagementQueryHandler : IRequestHandler<GetHotelsForManagementQuery,
  PaginatedList<HotelForManagementResponse>>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;

  public GetHotelsForManagementQueryHandler(IHotelRepository hotelRepository, IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedList<HotelForManagementResponse>> Handle(
    GetHotelsForManagementQuery request,
    CancellationToken cancellationToken)
  {
    var query = new PaginationQuery<Hotel>(
      GetSearchExpression(request.SearchTerm),
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _hotelRepository.GetForManagementAsync(query, cancellationToken);

    return _mapper.Map<PaginatedList<HotelForManagementResponse>>(owners);
  }

  private static Expression<Func<Hotel, bool>> GetSearchExpression(string? searchTerm)
  {
    return searchTerm is null
      ? _ => true
      : h => h.Name.Contains(searchTerm) || h.City.Name.Contains(searchTerm);
  }
}