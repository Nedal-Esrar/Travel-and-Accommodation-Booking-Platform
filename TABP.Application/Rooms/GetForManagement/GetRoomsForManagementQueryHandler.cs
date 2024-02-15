using System.Linq.Expressions;
using AutoMapper;
using MediatR;
using TABP.Application.Extensions;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;
using TABP.Domain.Models;

namespace TABP.Application.Rooms.GetForManagement;

public class GetRoomsHandler : IRequestHandler<GetRoomsForManagementQuery, PaginatedList<RoomForManagementResponse>>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;

  public GetRoomsHandler(
    IRoomRepository roomRepository,
    IMapper mapper,
    IRoomClassRepository roomClassRepository)
  {
    _roomRepository = roomRepository;
    _mapper = mapper;
    _roomClassRepository = roomClassRepository;
  }

  public async Task<PaginatedList<RoomForManagementResponse>> Handle(GetRoomsForManagementQuery request,
    CancellationToken cancellationToken)
  {
    if (!await _roomClassRepository.ExistsByIdAsync(request.RoomClassId, cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }

    var query = new PaginationQuery<Room>(
      GetSearchExpression(request.SearchTerm)
        .And(r => r.RoomClassId == request.RoomClassId),
      request.SortOrder ?? SortOrder.Ascending,
      request.SortColumn,
      request.PageNumber,
      request.PageSize);

    var owners = await _roomRepository.GetForManagementAsync(query, cancellationToken);

    return _mapper.Map<PaginatedList<RoomForManagementResponse>>(owners);
  }

  private static Expression<Func<Room, bool>> GetSearchExpression(string? searchTerm)
  {
    return searchTerm is null
      ? _ => true
      : r => r.Number.Contains(searchTerm);
  }
}