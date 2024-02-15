using TABP.Domain.Enums;

namespace TABP.Api.Dtos.Bookings;

public record BookingCreationRequest(
  List<Guid> RoomIds,
  Guid HotelId,
  DateOnly CheckInDateUtc,
  DateOnly CheckOutDateUtc,
  string? GuestRemarks,
  PaymentMethod PaymentMethod);