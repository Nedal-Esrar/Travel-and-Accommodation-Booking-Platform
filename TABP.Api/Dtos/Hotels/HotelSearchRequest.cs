using TABP.Api.Dtos.Common;
using TABP.Domain.Enums;

namespace TABP.Api.Dtos.Hotels;

/// <param name="SortColumn">Should be empty, 'id', 'name', 'starRating', 'price', or 'ReviewsRating'.</param>
public record HotelSearchRequest(
  string? SearchTerm,
  string? SortOrder,
  string? SortColumn,
  DateOnly CheckInDateUtc,
  DateOnly CheckOutDateUtc,
  int NumberOfAdults,
  int NumberOfChildren,
  int NumberOfRooms,
  decimal? MinPrice,
  decimal? MaxPrice,
  int? MinStarRating,
  IEnumerable<RoomType>? RoomTypes,
  IEnumerable<Guid>? Amenities,
  int PageNumber = 1,
  int PageSize = 10) :
  ResourcesQueryRequest(
    SearchTerm,
    SortOrder,
    SortColumn,
    PageNumber,
    PageSize);