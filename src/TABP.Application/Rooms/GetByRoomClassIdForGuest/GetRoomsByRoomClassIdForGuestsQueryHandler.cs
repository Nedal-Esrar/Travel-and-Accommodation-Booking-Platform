using AutoMapper;
using MediatR;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;
using TABP.Domain.Models;

namespace TABP.Application.Rooms.GetByRoomClassIdForGuest;

public class GetRoomsByRoomClassIdForGuestsQueryHandler :
  IRequestHandler<GetRoomsByRoomClassIdForGuestsQuery,
    PaginatedList<RoomForGuestResponse>>
{
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;
  private readonly IRoomRepository _roomRepository;

  public GetRoomsByRoomClassIdForGuestsQueryHandler(IRoomClassRepository roomClassRepository,
    IRoomRepository roomRepository, IMapper mapper)
  {
    _roomClassRepository = roomClassRepository;
    _roomRepository = roomRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedList<RoomForGuestResponse>> Handle(
    GetRoomsByRoomClassIdForGuestsQuery request,
    CancellationToken cancellationToken = default)
  {
    if (!await _roomClassRepository.ExistsAsync(
          rc => rc.Id == request.RoomClassId, 
          cancellationToken))
    {
      throw new NotFoundException(RoomClassMessages.NotFound);
    }

    var query = new PaginationQuery<Room>(
      r => r.RoomClassId == request.RoomClassId &&
           !r.Bookings.Any(b => request.CheckInDate >= b.CheckOutDateUtc
                                || request.CheckOutDate <= b.CheckInDateUtc),
      SortOrder.Ascending,
      null,
      request.PageNumber,
      request.PageSize);

    var rooms = await _roomRepository.GetAsync(query, cancellationToken);

    return _mapper.Map<PaginatedList<RoomForGuestResponse>>(rooms);
  }
}