using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Images;
using TABP.Api.Dtos.RoomClasses;
using TABP.Api.Extensions;
using TABP.Application.RoomClasses.AddImageToGallery;
using TABP.Application.RoomClasses.Create;
using TABP.Application.RoomClasses.Delete;
using TABP.Application.RoomClasses.GetForManagement;
using TABP.Application.RoomClasses.Update;
using TABP.Domain;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/room-classes")]
[ApiVersion("1.0")]
[Authorize(Roles = UserRoles.Admin)]
public class RoomClassesController(ISender mediator, IMapper mapper) : ControllerBase
{
  /// <summary>
  ///   Retrieve a page of room classes based on the provided parameters for management (admin).
  /// </summary>
  /// <param name="roomClassesGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The request page of room classes for management, with pagination metadata included.</returns>
  /// <response code="200">Returns the request page of room classes for management, with pagination metadata included.</response>
  /// <response code="400">If the request parameters are invalid or missing.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<RoomClassForManagementResponse>>> GetRoomClassesForManagement(
    [FromQuery] RoomClassesGetRequest roomClassesGetRequest,
    CancellationToken cancellationToken)
  {
    var query = mapper.Map<GetRoomClassesForManagementQuery>(roomClassesGetRequest);

    var roomClasses = await mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(roomClasses.PaginationMetadata);

    return Ok(roomClasses.Items);
  }

  /// <summary>
  ///   Create a new room class.
  /// </summary>
  /// <param name="roomClassCreationRequest">The data of the new room class.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="201">If the room class was created successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If one of the provided amenities is not found.</response>
  /// <response code="409">If there is a room class with the same name in the same hotel.</response>
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateRoomClass(
    RoomClassCreationRequest roomClassCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = mapper.Map<CreateRoomClassCommand>(roomClassCreationRequest);

    await mediator.Send(command, cancellationToken);

    return Created();
  }

  /// <summary>
  ///   Update an existing room class specified by ID.
  /// </summary>
  /// <param name="id">The ID of the room class.</param>
  /// <param name="roomClassUpdateRequest">The updated data of the room class.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the room class was updated successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the room class was not found.</response>
  /// <response code="409">If there is a room class with the same name in the same hotel.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateRoomClass(
    Guid id,
    RoomClassUpdateRequest roomClassUpdateRequest,
    CancellationToken cancellationToken)
  {
    var command = new UpdateRoomClassCommand { RoomClassId = id };
    mapper.Map(roomClassUpdateRequest, command);

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Delete an existing room class specified by ID.
  /// </summary>
  /// <param name="id">The ID of the room class.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the room class was deleted successfully.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the room class was not found.</response>
  /// <response code="409">If there are rooms in the room class.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteRoomClass(
    Guid id,
    CancellationToken cancellationToken)
  {
    var command = new DeleteRoomClassCommand { RoomClassId = id };

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Add a new image to a room class's gallery specified by ID.
  /// </summary>
  /// <param name="id">The ID of the room class</param>
  /// <param name="imageCreationRequest">The new image to add.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the image was added to the gallery successfully.</response>
  /// <response code="400">If the image is invalid (not .jpg, .jpeg, or .png).</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the room class specified by ID is not found.</response>
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
    var command = new AddImageToRoomClassGalleryCommand { RoomClassId = id };
    mapper.Map(imageCreationRequest, command);

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }
}