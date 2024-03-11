using System.Security.Claims;
using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Reviews;
using TABP.Application.Reviews.Common;
using TABP.Application.Reviews.Create;
using TABP.Application.Reviews.Delete;
using TABP.Application.Reviews.GetByHotelId;
using TABP.Application.Reviews.GetById;
using TABP.Application.Reviews.Update;
using TABP.Domain;
using TABP.Domain.Enums;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/hotels/{hotelId:guid}/reviews")]
[ApiVersion("1.0")]
[Authorize(Roles = UserRoles.Guest)]
public class ReviewsController(ISender mediator, IMapper mapper) : ControllerBase
{
  /// <summary>
  ///   Retrieve a page of reviews for a hotel specified by ID based on the provided parameters.
  /// </summary>
  /// <param name="hotelId">The ID of the hotel.</param>
  /// <param name="reviewsGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The request page of reviews, with pagination metadata included.</returns>
  /// <response code="200">Returns the request page of reviews, with pagination metadata included.</response>
  /// <response code="400">If the request parameters are invalid or missing.</response>
  /// <response code="404">If the hotel specified by ID was not found.</response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<ReviewResponse>>> GetReviewsForHotel(
    Guid hotelId,
    [FromQuery] ReviewsGetRequest reviewsGetRequest,
    CancellationToken cancellationToken)
  {
    var query = new GetReviewsByHotelIdQuery { HotelId = hotelId };
    mapper.Map(reviewsGetRequest, query);

    var reviews = await mediator.Send(query, cancellationToken);

    Response.Headers["x-pagination"] = JsonSerializer.Serialize(
      reviews.PaginationMetadata);

    return Ok(reviews.Items);
  }

  /// <summary>
  ///   Get a review specified by ID for a hotel specified by ID.
  /// </summary>
  /// <param name="hotelId">The ID of the hotel.</param>
  /// <param name="id">The ID of the review.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested review.</returns>
  /// <response code="200">The requested review.</response>
  /// <response code="404">If the hotel specified by ID was not found or the review with ID is not found in the hotel.</response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}")]
  [AllowAnonymous]
  public async Task<ActionResult<ReviewResponse>> GetReviewById(
    Guid hotelId, Guid id, CancellationToken cancellationToken = default)
  {
    var query = new GetReviewByIdQuery
    {
      HotelId = hotelId,
      ReviewId = id
    };

    var review = await mediator.Send(query, cancellationToken);

    return Ok(review);
  }

  /// <summary>
  ///   Create a new review for a hotel specified by ID.
  /// </summary>
  /// <param name="hotelId">The ID of the hotel.</param>
  /// <param name="reviewCreationRequest">The data of the new review.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The newly created review</returns>
  /// <response code="201">If the review was created successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not a guest).</response>
  /// <response code="404">If the hotel specified by ID is not found.</response>
  /// <response code="409">
  ///   If The guest did not book a room in the hotel yet, or
  ///   If the guest has already reviewed the hotel.
  /// </response>
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateReviewForHotel(Guid hotelId,
    ReviewCreationRequest reviewCreationRequest,
    CancellationToken cancellationToken)
  {
    var guestId = Guid.Parse(
      User.FindFirstValue(ClaimTypes.NameIdentifier) ??
      throw new ArgumentNullException());

    var command = new CreateReviewCommand
    {
      GuestId = guestId, 
      HotelId = hotelId
    };
    mapper.Map(reviewCreationRequest, command);

    var createdReview = await mediator.Send(command, cancellationToken);
    
    return CreatedAtAction(nameof(GetReviewById), new { id = createdReview }, createdReview);
  }

  /// <summary>
  ///   Update an existing review specified by ID for a hotel specified by ID.
  /// </summary>
  /// <param name="hotelId">The ID of the hotel.</param>
  /// <param name="id">The ID of the review.</param>
  /// <param name="reviewUpdateRequest">The updated data of the review.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the hotel was updated successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not a guest).</response>
  /// <response code="404">
  ///   If the hotel was not found or the review for the guest in the hotel is not found.
  /// </response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateReviewForHotel(
    Guid hotelId, Guid id,
    ReviewUpdateRequest reviewUpdateRequest,
    CancellationToken cancellationToken)
  {
    var guestId = Guid.Parse(
      User.FindFirstValue(ClaimTypes.NameIdentifier) ??
      throw new ArgumentNullException());

    var command = new UpdateReviewCommand
    {
      GuestId = guestId,
      HotelId = hotelId,
      ReviewId = id
    };
    mapper.Map(reviewUpdateRequest, command);

    await mediator.Send(
      command,
      cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Delete an existing review specified by ID for a hotel specified by ID.
  /// </summary>
  /// <param name="hotelId">The ID of the hotel.</param>
  /// <param name="id">The ID of the review.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the review was deleted successfully.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not a guest).</response>
  /// <response code="404">
  ///   If the hotel was not found or the review for the guest in the hotel is not found.
  /// </response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteReviewForHotel(
    Guid hotelId, Guid id,
    CancellationToken cancellationToken)
  {
    var guestId = Guid.Parse(
      User.FindFirstValue(ClaimTypes.NameIdentifier)
      ?? throw new ArgumentNullException());

    var command = new DeleteReviewCommand
    {
      GuestId = guestId,
      HotelId = hotelId,
      ReviewId = id
    };

    await mediator.Send(
      command,
      cancellationToken);

    return NoContent();
  }
}