using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Hotels;
using TABP.Api.Dtos.Images;
using TABP.Api.Dtos.RoomClasses;
using TABP.Application.Hotels.AddToGallery;
using TABP.Application.Hotels.Create;
using TABP.Application.Hotels.Delete;
using TABP.Application.Hotels.GetFeaturedDeals;
using TABP.Application.Hotels.GetForGuestById;
using TABP.Application.Hotels.GetForManagement;
using TABP.Application.Hotels.Search;
using TABP.Application.Hotels.SetThumbnail;
using TABP.Application.Hotels.Update;
using TABP.Application.RoomClasses.GetByHotelIdForGuest;
using TABP.Domain;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/hotels")]
[ApiVersion("1.0")]
[Authorize(Roles = UserRoles.Admin)]
public class HotelsController(ISender mediator, IMapper mapper) : ControllerBase
{
  /// <summary>
  ///   Retrieve a page of hotels based on the provided parameters for management (admin).
  /// </summary>
  /// <param name="hotelsGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The request page of hotels for management, with pagination metadata included.</returns>
  /// <response code="200">Returns the request page of hotels for management, with pagination metadata included.</response>
  /// <response code="400">If the request parameters are invalid or missing.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  [ProducesResponseType(typeof(IEnumerable<HotelForManagementResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<HotelForManagementResponse>>> GetHotelsForManagement(
    [FromQuery] HotelsGetRequest hotelsGetRequest,
    CancellationToken cancellationToken)
  {
    var query = mapper.Map<GetHotelsForManagementQuery>(hotelsGetRequest);

    var hotels = await mediator.Send(query, cancellationToken);

    Response.Headers["x-pagination"] = JsonSerializer.Serialize(
      hotels.PaginationMetadata);

    return Ok(hotels.Items);
  }

  /// <summary>
  ///   Search and filter hotels based on specific criteria.
  /// </summary>
  /// <param name="hotelSearchRequest">The criteria for searching and filtering.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested page of hotels based on the provided criteria, with pagination metadata included.</returns>
  /// <response code="200">requested page of hotels based on the provided criteria, with pagination metadata included.</response>
  /// <response code="400">If the request data is invalid or missing.</response>
  [ProducesResponseType(typeof(IEnumerable<HotelSearchResultResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [HttpGet("search")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<HotelSearchResultResponse>>> SearchAndFilterHotels(
    [FromQuery] HotelSearchRequest hotelSearchRequest,
    CancellationToken cancellationToken = default)
  {
    var query = mapper.Map<SearchForHotelsQuery>(hotelSearchRequest);

    var hotels = await mediator.Send(query, cancellationToken);

    Response.Headers["x-pagination"] = JsonSerializer.Serialize(
      hotels.PaginationMetadata);

    return Ok(hotels.Items);
  }

  /// <summary>
  ///   Retrieve N hotel featured deals.
  /// </summary>
  /// <param name="hotelFeaturedDealsGetRequest">The request number of featured deals (N).</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested number of hotel featured deals.</returns>
  /// <response code="200">Returns the requested number of hotel featured deals.</response>
  /// <response code="400">If the count is less than 1 or greater than 100.</response>
  [ProducesResponseType(typeof(IEnumerable<HotelFeaturedDealResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [HttpGet("featured-deals")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<HotelFeaturedDealResponse>>> GetFeaturedDeals(
    [FromQuery] HotelFeaturedDealsGetRequest hotelFeaturedDealsGetRequest,
    CancellationToken cancellationToken = default)
  {
    var query = mapper.Map<GetHotelFeaturedDealsQuery>(hotelFeaturedDealsGetRequest);

    var featuredDeals = await mediator.Send(query, cancellationToken);

    return Ok(featuredDeals);
  }

  /// <summary>
  ///   Get hotel by ID for guest.
  /// </summary>
  /// <param name="id">The ID of the hotel.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested hotel specified by ID.</returns>
  /// <response code="200">Returns the requested hotel specified by ID.</response>
  /// <response code="404">If the hotel with the specified ID is not found.</response>
  [ProducesResponseType(typeof(HotelForGuestResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}")]
  [AllowAnonymous]
  public async Task<ActionResult<HotelForGuestResponse>> GetHotelForGuest(Guid id,
    CancellationToken cancellationToken = default)
  {
    var query = new GetHotelForGuestByIdQuery { HotelId = id };

    var hotel = await mediator.Send(query, cancellationToken);

    return Ok(hotel);
  }

  /// <summary>
  ///   Get room classes for an hotel specified by ID for Guests based on the provided parameters.
  /// </summary>
  /// <param name="id">The ID of the hotel.</param>
  /// <param name="getRoomClassesForGuestRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>the requested room classes for the specified hotel, with pagination metadata included..</returns>
  /// <response code="200">Returns the requested room classes for the specified hotel, with pagination metadata included..</response>
  /// <response code="404">If the specified hotel by ID was not found.</response>
  [ProducesResponseType(typeof(IEnumerable<RoomClassForGuestResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}/room-classes")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<RoomClassForGuestResponse>>> GetRoomClassesForGuests(
    Guid id,
    [FromQuery] GetRoomClassesForGuestRequest getRoomClassesForGuestRequest,
    CancellationToken cancellationToken = default)
  {
    var query = new GetRoomClassesByHotelIdForGuestQuery { HotelId = id };
    mapper.Map(getRoomClassesForGuestRequest, query);

    var roomClasses = await mediator.Send(query, cancellationToken);

    Response.Headers["x-pagination"] = JsonSerializer.Serialize(
      roomClasses.PaginationMetadata);

    return Ok(roomClasses.Items);
  }

  /// <summary>
  ///   Create a new hotel.
  /// </summary>
  /// <param name="hotelCreationRequest">The data of the new hotel.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="201">If the hotel was created successfully.</response>
  /// <response code="400">If the request data is invalid or missing.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="409">If there is an hotel in the same geographical location (longitude and latitude)</response>
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateHotel(
    HotelCreationRequest hotelCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = mapper.Map<CreateHotelCommand>(hotelCreationRequest);

    await mediator.Send(command, cancellationToken);

    return Created();
  }

  /// <summary>
  ///   Update an existing hotel specified by ID.
  /// </summary>
  /// <param name="id">The ID of the hotel.</param>
  /// <param name="hotelUpdateRequest">The updated data of the hotel.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the hotel was updated successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the hotel was not found.</response>
  /// <response code="409">If there is an hotel in the same geographical location (longitude and latitude)</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateHotel(
    Guid id,
    HotelUpdateRequest hotelUpdateRequest,
    CancellationToken cancellationToken)
  {
    var command = new UpdateHotelCommand { HotelId = id };
    mapper.Map(hotelUpdateRequest, command);

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Delete an existing hotel specified by ID.
  /// </summary>
  /// <param name="id">The ID of the hotel.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the hotel was deleted successfully.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the hotel was not found.</response>
  /// <response code="409">If there are room classes in the hotel.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteHotel(
    Guid id,
    CancellationToken cancellationToken)
  {
    var command = new DeleteHotelCommand { HotelId = id };

    await mediator.Send(
      command,
      cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Set the thumbnail of an hotel specified by ID.
  /// </summary>
  /// <param name="id">The ID of the hotel</param>
  /// <param name="imageCreationRequest">The new thumbnail.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the thumbnail was set successfully.</response>
  /// <response code="400">If the thumbnail is invalid (not .jpg, .jpeg, or .png).</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the hotel specified by ID is not found.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpPut("{id:guid}/thumbnail")]
  public async Task<IActionResult> SetHotelThumbnail(
    Guid id,
    [FromForm] ImageCreationRequest imageCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = new SetHotelThumbnailCommand { HotelId = id };
    mapper.Map(imageCreationRequest, command);
    
    await mediator.Send(command, cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Add a new image to an hotel's gallery specified by ID.
  /// </summary>
  /// <param name="id">The ID of the hotel</param>
  /// <param name="imageCreationRequest">The new image to add.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the image was added to the gallery successfully.</response>
  /// <response code="400">If the image is invalid (not .jpg, .jpeg, or .png).</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the hotel specified by ID is not found.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpPost("{id:guid}/gallery")]
  public async Task<IActionResult> AddImageToHotelGallery(
    Guid id,
    [FromForm] ImageCreationRequest imageCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = new AddImageToHotelGalleryCommand { HotelId = id };
    mapper.Map(imageCreationRequest, command);

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }
}