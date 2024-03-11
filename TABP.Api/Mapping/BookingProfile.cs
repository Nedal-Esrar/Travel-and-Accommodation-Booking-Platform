using AutoMapper;
using TABP.Api.Dtos.Bookings;
using TABP.Application.Bookings.Create;
using TABP.Application.Bookings.GetForGuest;
using static TABP.Api.Mapping.MappingUtilities;

namespace TABP.Api.Mapping;

public class BookingProfile : Profile
{
  public BookingProfile()
  {
    CreateMap<BookingCreationRequest, CreateBookingCommand>();

    CreateMap<BookingsGetRequest, GetBookingsQuery>()
      .ForMember(dst => dst.SortOrder, opt => opt.MapFrom(src => MapToSortOrder(src.SortOrder)));
  }
}