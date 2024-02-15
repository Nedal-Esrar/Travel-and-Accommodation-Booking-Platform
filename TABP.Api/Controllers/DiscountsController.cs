using System.Text.Json;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Discounts;
using TABP.Application.Discounts.Create;
using TABP.Application.Discounts.Delete;
using TABP.Application.Discounts.Get;
using TABP.Application.Discounts.GetById;
using TABP.Domain;
using TABP.Domain.Enums;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/room-classes/{roomClassId:guid}/discounts")]
[ApiVersion("1.0")]
[Authorize(Roles = UserRoles.Admin)]
public class DiscountsController(ISender mediator) : ControllerBase
{
  /// <summary>
  ///   Retrieve a page of discounts for a room class specified by ID based on the provided parameters.
  /// </summary>
  /// <param name="roomClassId">Room class ID.</param>
  /// <param name="discountsGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested page of discounts, with pagination metadata included.</returns>
  /// <response code="200">Returns the request page of amenities, with pagination metadata included.</response>
  /// <response code="400">If the request parameters are invalid or missing.</response>
  /// <response code="404">If the room class with ID is not found.</response>
  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<DiscountResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<IEnumerable<DiscountResponse>>> GetAmenities(
    Guid roomClassId,
    [FromQuery] DiscountsGetRequest discountsGetRequest,
    CancellationToken cancellationToken)
  {
    var sortOrder = discountsGetRequest.SortOrder switch
    {
      "asc" => SortOrder.Ascending,
      "desc" => SortOrder.Descending,
      _ => (SortOrder?)null
    };

    var query = new GetDiscountsQuery(
      roomClassId,
      sortOrder,
      discountsGetRequest.SortColumn,
      discountsGetRequest.PageNumber,
      discountsGetRequest.PageSize);

    var discounts = await mediator.Send(query, cancellationToken);

    Response.Headers["x-pagination"] = JsonSerializer.Serialize(
      discounts.PaginationMetadata);

    return Ok(discounts.Items);
  }

  /// <summary>
  ///   Get an existing discount by ID.
  /// </summary>
  /// <param name="roomClassId">The ID of the room class of the requested discount.</param>
  /// <param name="id">The ID of the requested discount.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The discount specified by ID.</returns>
  /// <response code="200">Returns the discount specified by ID.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">
  ///   If the room class with the given ID is not found or
  ///   the discount with the given ID is not found in room class.
  /// </response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<DiscountResponse>> GetDiscount(
    Guid roomClassId,
    Guid id,
    CancellationToken cancellationToken)
  {
    var query = new GetDiscountByIdQuery(
      roomClassId,
      id);

    var discount = await mediator.Send(query,
      cancellationToken);

    return Ok(discount);
  }

  /// <summary>
  ///   Create a discount for a room class specified by ID.
  /// </summary>
  /// <param name="roomClassId">The ID of the room class.</param>
  /// <param name="discountCreationRequest">The data of the new discount.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="201">If the discount was created successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the room class is not found.</response>
  /// <response code="409">If another discount intersects with the new discount in date intervals</response>
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateDiscount(
    Guid roomClassId,
    DiscountCreationRequest discountCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = new CreateDiscountCommand(
      roomClassId,
      discountCreationRequest.Percentage,
      discountCreationRequest.StartDateUtc,
      discountCreationRequest.EndDateUtc);

    var createdDiscount = await mediator.Send(command, cancellationToken);

    return CreatedAtAction(nameof(GetDiscount), new { id = createdDiscount.Id }, createdDiscount);
  }

  /// <summary>
  ///   Delete an existing discount specified by ID.
  /// </summary>
  /// <param name="roomClassId">The ID of the room class.</param>
  /// <param name="id">The id of the discount to update.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the discount is deleted successfully.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">
  ///   If the room class is not found or
  ///   discount is not found in room class.
  /// </response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteDiscount(
    Guid roomClassId,
    Guid id,
    CancellationToken cancellationToken)
  {
    var command = new DeleteDiscountCommand(roomClassId, id);

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }
}