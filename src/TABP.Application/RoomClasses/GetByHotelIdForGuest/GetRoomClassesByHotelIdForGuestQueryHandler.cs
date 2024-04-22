using AutoMapper;
using MediatR;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;
using TABP.Domain.Models;

namespace TABP.Application.RoomClasses.GetByHotelIdForGuest;

public class GetRoomClassesByHotelIdForGuestQueryHandler : IRequestHandler<GetRoomClassesByHotelIdForGuestQuery,
  PaginatedList<RoomClassForGuestResponse>>
{
  private readonly IHotelRepository _hotelRepository;
  private readonly IMapper _mapper;
  private readonly IRoomClassRepository _roomClassRepository;

  public GetRoomClassesByHotelIdForGuestQueryHandler(IHotelRepository hotelRepository,
    IRoomClassRepository roomClassRepository, IMapper mapper)
  {
    _hotelRepository = hotelRepository;
    _roomClassRepository = roomClassRepository;
    _mapper = mapper;
  }

  public async Task<PaginatedList<RoomClassForGuestResponse>> Handle(GetRoomClassesByHotelIdForGuestQuery request,
    CancellationToken cancellationToken = default)
  {
    if (!await _hotelRepository.ExistsAsync(h => h.Id == request.HotelId, cancellationToken))
    {
      throw new NotFoundException(HotelMessages.NotFound);
    }

    var roomClasses = await _roomClassRepository.GetAsync(
      new PaginationQuery<RoomClass>(
        rc => rc.HotelId == request.HotelId,
        request.SortOrder ?? SortOrder.Ascending,
        request.SortColumn,
        request.PageNumber,
        request.PageSize),
      includeGallery: true,
      cancellationToken);

    return _mapper.Map<PaginatedList<RoomClassForGuestResponse>>(roomClasses);
  }
}