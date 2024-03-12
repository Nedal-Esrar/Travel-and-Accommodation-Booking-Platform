namespace TABP.Api.Validators.Hotels;

public static class HotelValidationMessages
{
  public const string RoomTypeNotValid =
    "provided RoomType is not valid.";

  public const string SearchSortColumnNotValid =
    "Sort Column must be empty or 'id' or 'name' or 'reviewsRating' or 'price' or 'starRating'";

  public const string GetSortColumnNotValid =
    "Sort Column must be empty, 'id', or 'name'";
}