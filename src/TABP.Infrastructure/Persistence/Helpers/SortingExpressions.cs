using System.Linq.Expressions;
using TABP.Domain.Entities;
using TABP.Domain.Models;

namespace TABP.Infrastructure.Persistence.Helpers;

public static class SortingExpressions
{
  public static Expression<Func<Amenity, object>> GetAmenitySortExpression(
    string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => a => a.Id,
      "name" => a => a.Name,
      _ => o => o.Id
    };
  }

  public static Expression<Func<Discount, object>> GetDiscountSortExpression(string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => d => d.Id,
      "creationdate" => d => d.CreatedAtUtc,
      "startdate" => d => d.StartDateUtc,
      "enddate" => d => d.EndDateUtc,
      _ => d => d.Id
    };
  }
  
  public static Expression<Func<Room, object>> GetRoomSortExpression(string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => r => r.Id,
      "number" => r => r.Number,
      _ => r => r.Id
    };
  }
  
  public static Expression<Func<RoomClass, object>> GetRoomClassSortExpression(
    string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => rc => rc.Id,
      "name" => rc => rc.Name,
      "adultscapacity" => rc => rc.AdultsCapacity,
      "childrencapacity" => rc => rc.ChildrenCapacity,
      "pricepernight" => rc => rc.PricePerNight,
      _ => rc => rc.Id
    };
  }
  
  public static Expression<Func<Owner, object>> GetOwnerSortExpression(
    string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => o => o.Id,
      "firstname" => o => o.FirstName,
      "lastname" => o => o.LastName,
      _ => o => o.Id
    };
  }
  
  public static Expression<Func<Hotel, object>> GetHotelSortExpression(
    string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => o => o.Id,
      "name" => o => o.Name,
      _ => o => o.Id
    };
  }
  
  public static Expression<Func<HotelSearchResult, object>> GetHotelSearchSortExpression(string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => h => h.Id,
      "name" => h => h.Name,
      "starrating" => h => h.StarRating,
      "price" => h => h.PricePerNightStartingAt,
      "reviewsrating" => h => h.ReviewsRating,
      _ => h => h.Id
    };
  }
  
  public static Expression<Func<City, object>> GetCitySortExpression(
    string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => o => o.Id,
      "name" => o => o.Name,
      "country" => o => o.Country,
      "postoffice" => o => o.PostOffice,
      _ => o => o.Id
    };
  }

  public static Expression<Func<Review, object>> GetReviewSortExpression(string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => r => r.Id,
      "rating" => r => r.Rating,
      _ => r => r.Id
    };
  }

  public static Expression<Func<Booking, object>> GetBookingSortExpression(string? sortColumn)
  {
    return sortColumn?.ToLower() switch
    {
      "id" => b => b.Id,
      "bookingdate" => b => b.BookingDateUtc,
      "checkindate" => b => b.CheckInDateUtc,
      "checkoutdate" => b => b.CheckOutDateUtc,
      _ => b => b.Id
    };
  }
}