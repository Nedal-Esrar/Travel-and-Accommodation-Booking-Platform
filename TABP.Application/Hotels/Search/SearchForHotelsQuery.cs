using MediatR;
using TABP.Domain.Enums;
using TABP.Domain.Models;

namespace TABP.Application.Hotels.Search;

public record SearchForHotelsQuery(
  string? SearchTerm,
  SortOrder? SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize,
  DateOnly CheckInDate,
  DateOnly CheckOutDate,
  int NumberOfAdults,
  int NumberOfChildren,
  int NumberOfRooms,
  decimal? MinPrice,
  decimal? MaxPrice,
  int? MinStarRating,
  IEnumerable<RoomType> RoomTypes,
  IEnumerable<Guid> Amenities) : IRequest<PaginatedList<HotelSearchResultResponse>>;