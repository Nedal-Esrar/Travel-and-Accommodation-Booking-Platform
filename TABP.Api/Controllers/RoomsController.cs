using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Rooms;
using TABP.Api.Extensions;
using TABP.Application.Rooms.Create;
using TABP.Application.Rooms.Delete;
using TABP.Application.Rooms.GetByRoomClassIdForGuest;
using TABP.Application.Rooms.GetForManagement;
using TABP.Application.Rooms.Update;
using TABP.Domain;
using TABP.Domain.Enums;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/room-classes/{roomClassId:guid}/rooms")]
[ApiVersion("1.0")]
[Authorize(Roles = UserRoles.Admin)]
public class RoomsController(ISender mediator, IMapper mapper) : ControllerBase
{
  /// <summary>
  ///   Retrieve a page of rooms for a room class based on the provided parameters for management (admin).
  /// </summary>
  /// <param name="roomClassId">The ID of the room class</param>
  /// <param name="roomsGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The request page of rooms, with pagination metadata included.</returns>
  /// <response code="200">Returns the request page of rooms, with pagination metadata included.</response>
  /// <response code="400">If the request parameters are invalid or missing.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the room class is not found.</response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<RoomForManagementResponse>>> GetRoomsForManagement(
    Guid roomClassId,
    [FromQuery] RoomsGetRequest roomsGetRequest,
    CancellationToken cancellationToken)
  {
    var query = new GetRoomsForManagementQuery { RoomClassId = roomClassId };
    mapper.Map(roomsGetRequest, query);

    var rooms = await mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(rooms.PaginationMetadata);

    return Ok(rooms.Items);
  }

  /// <summary>
  ///   Retrieve a page of available rooms for a room class based on the provided parameters for guests.
  /// </summary>
  /// <param name="roomClassId">The ID of the room class</param>
  /// <param name="roomsForGuestsGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested page of available rooms, with pagination metadata included.</returns>
  /// <response code="200">Returns the request page of available rooms, with pagination metadata included.</response>
  /// <response code="400">If the request parameters are invalid or missing.</response>
  /// <response code="404">If the room class is not found.</response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("available")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<RoomForGuestResponse>>> GetRoomsForGuests(
    Guid roomClassId,
    [FromQuery] RoomsForGuestsGetRequest roomsForGuestsGetRequest,
    CancellationToken cancellationToken)
  {
    var query = new GetRoomsByRoomClassIdForGuestsQuery { RoomClassId = roomClassId };
    mapper.Map(roomsForGuestsGetRequest, query);

    var rooms = await mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(rooms.PaginationMetadata);

    return Ok(rooms.Items);
  }

  /// <summary>
  ///   Create a new room in a room class specified by ID.
  /// </summary>
  /// <param name="roomClassId">The ID of the room class</param>
  /// <param name="roomCreationRequest">The data of the new room.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="201">If the room was created successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the room class is not found.</response>
  /// <response code="409">If there is a room with the same number in the room class of the room class.</response>
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateRoomInRoomClass(
    Guid roomClassId,
    RoomCreationRequest roomCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = new CreateRoomCommand { RoomClassId = roomClassId };
    mapper.Map(roomCreationRequest, command);

    await mediator.Send(command, cancellationToken);

    return Created();
  }

  /// <summary>
  ///   Update an existing room with ID in a room class specified by ID.
  /// </summary>
  /// <param name="roomClassId">The ID of the room class.</param>
  /// <param name="id">The ID of the room.</param>
  /// <param name="roomUpdateRequest">The updated data of the room.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the room was updated successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the room class is not found or room is not found in room class.</response>
  /// <response code="409">If there is a room with the same number in the room class of the room class.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateRoomInRoomClass(
    Guid roomClassId, Guid id,
    RoomUpdateRequest roomUpdateRequest,
    CancellationToken cancellationToken)
  {
    var command = new UpdateRoomCommand
    {
      RoomClassId = roomClassId,
      RoomId = id
    };
    mapper.Map(roomUpdateRequest, command);

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Delete a room by ID in a room class specified by ID.
  /// </summary>
  /// <param name="roomClassId">The ID of the room class</param>
  /// <param name="id">The ID of the room.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the room was deleted successfully.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the room class is not found or room is not found in room class.</response>
  /// <response code="409">If there are bookings to the room.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteRoomInRoomClass(Guid roomClassId,
    Guid id,
    CancellationToken cancellationToken = default)
  {
    var command = new DeleteRoomCommand
    {
      RoomClassId = roomClassId,
      RoomId = id
    };

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }
}