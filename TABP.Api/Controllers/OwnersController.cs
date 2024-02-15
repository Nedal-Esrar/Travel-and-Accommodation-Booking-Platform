using System.Text.Json;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Owners;
using TABP.Application.Owners.Common;
using TABP.Application.Owners.Create;
using TABP.Application.Owners.Get;
using TABP.Application.Owners.GetById;
using TABP.Application.Owners.Update;
using TABP.Domain;
using TABP.Domain.Enums;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/owners")]
[Authorize(Roles = UserRoles.Admin)]
[ApiVersion("1.0")]
public class OwnersController(ISender mediator) : ControllerBase
{
  /// <summary>
  ///   Retrieve a page of owners based on the provided parameters.
  /// </summary>
  /// <param name="ownersGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The request page of owners, with pagination metadata included.</returns>
  /// <response code="200">Returns the request page of owners, with pagination metadata included.</response>
  /// <response code="400">If the request parameters are invalid or missing.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<OwnerResponse>>> GetOwners(
    [FromQuery] OwnersGetRequest ownersGetRequest,
    CancellationToken cancellationToken)
  {
    var sortOrder = ownersGetRequest.SortOrder switch
    {
      "asc" => SortOrder.Ascending,
      "desc" => SortOrder.Descending,
      _ => (SortOrder?)null
    };

    var query = new GetOwnersQuery(
      ownersGetRequest.SearchTerm,
      sortOrder,
      ownersGetRequest.SortColumn,
      ownersGetRequest.PageNumber,
      ownersGetRequest.PageSize);

    var owners = await mediator.Send(query, cancellationToken);

    Response.Headers["x-pagination"] = JsonSerializer.Serialize(
      owners.PaginationMetadata);

    return Ok(owners.Items);
  }

  /// <summary>
  ///   Get an existing owner by ID.
  /// </summary>
  /// <param name="id">The ID of the requested owner.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The owner with the given ID.</returns>
  /// <response code="200">Returns the owner with the given ID.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the owner with the given ID is not found.</response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<OwnerResponse>> GetOwner(Guid id,
    CancellationToken cancellationToken)
  {
    var query = new GetOwnerByIdQuery(id);

    var owner = await mediator.Send(query, cancellationToken);

    return Ok(owner);
  }

  /// <summary>
  ///   Create a new owner.
  /// </summary>
  /// <param name="ownerCreationRequest">The data of the new owner.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="201">If the owner was created successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpPost]
  public async Task<IActionResult> CreateOwner(
    OwnerCreationRequest ownerCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = new CreateOwnerCommand(
      ownerCreationRequest.FirstName,
      ownerCreationRequest.LastName,
      ownerCreationRequest.Email,
      ownerCreationRequest.PhoneNumber);

    var createdOwner = await mediator.Send(
      command,
      cancellationToken);

    return CreatedAtAction(nameof(GetOwner), new { id = createdOwner.Id }, createdOwner);
  }

  /// <summary>
  ///   Update an existing owner.
  /// </summary>
  /// <param name="id">The id of the owner to update.</param>
  /// <param name="ownerUpdateRequest">The updated data of the owner.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the owner is updated successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the owner is not found.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateOwner(Guid id, OwnerUpdateRequest ownerUpdateRequest, CancellationToken cancellationToken)
  {
    var command = new UpdateOwnerCommand(
      id,
      ownerUpdateRequest.FirstName,
      ownerUpdateRequest.LastName,
      ownerUpdateRequest.Email,
      ownerUpdateRequest.PhoneNumber);

    await mediator.Send(command);

    return NoContent();
  }
}