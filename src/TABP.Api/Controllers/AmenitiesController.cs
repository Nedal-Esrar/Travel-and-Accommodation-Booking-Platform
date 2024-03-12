using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Amenities;
using TABP.Api.Extensions;
using TABP.Application.Amenities.Common;
using TABP.Application.Amenities.Create;
using TABP.Application.Amenities.Get;
using TABP.Application.Amenities.GetById;
using TABP.Application.Amenities.Update;
using TABP.Domain;
using TABP.Domain.Enums;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/amenities")]
[ApiVersion("1.0")]
public class AmenitiesController(ISender mediator, IMapper mapper) : ControllerBase
{
  /// <summary>
  ///   Retrieve a page of amenities based on the provided parameters.
  /// </summary>
  /// <param name="amenitiesGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested page of amenities, with pagination metadata included.</returns>
  /// <response code="200">Returns the request page of amenities, with pagination metadata included.</response>
  /// <response code="400">If the request parameters are invalid or missing.</response>
  [HttpGet]
  [ProducesResponseType(typeof(IEnumerable<AmenityResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  public async Task<ActionResult<IEnumerable<AmenityResponse>>> GetAmenities(
    [FromQuery] AmenitiesGetRequest amenitiesGetRequest,
    CancellationToken cancellationToken)
  {
    var query = mapper.Map<GetAmenitiesQuery>(amenitiesGetRequest);

    var amenities = await mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(amenities.PaginationMetadata);

    return Ok(amenities.Items);
  }

  /// <summary>
  ///   Get an amenity specified by ID.
  /// </summary>
  /// <param name="id">Amenity ID.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested amenity specified by ID.</returns>
  /// <response code="200">The requested amenity specified ID.</response>
  /// <response code="404">If the amenity specified by ID is not found.</response>
  [HttpGet("{id:guid}")]
  [ProducesResponseType(typeof(AmenityResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<ActionResult<AmenityResponse>> GetAmenity(
    Guid id, CancellationToken cancellationToken)
  {
    var query = new GetAmenityByIdQuery { AmenityId = id };

    var amenity = await mediator.Send(query, cancellationToken);

    return Ok(amenity);
  }

  /// <summary>
  ///   Create a new amenity.
  /// </summary>
  /// <param name="amenityCreationRequest">The data of the new amenity.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The newly created amenity.</returns>
  /// <response code="201">If the amenity was added successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="409">If there exists an amenity with the same name.</response>
  [Authorize(Roles = UserRoles.Admin)]
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public async Task<IActionResult> CreateAmenity(
    AmenityCreationRequest amenityCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = mapper.Map<CreateAmenityCommand>(amenityCreationRequest);

    var createdAmenity = await mediator.Send(command, cancellationToken);

    return CreatedAtAction(nameof(GetAmenity), new { id = createdAmenity.Id }, createdAmenity);
  }

  /// <summary>
  ///   Update an existing amenity.
  /// </summary>
  /// <param name="id">The id of the amenity to update.</param>
  /// <param name="amenityUpdateRequest">The updated data of the amenity.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the amenity is updated successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the amenity is not found.</response>
  /// <response code="409">If there exists an amenity with the same updated name.</response>
  [Authorize(Roles = UserRoles.Admin)]
  [HttpPut("{id:guid}")]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public async Task<IActionResult> UpdateAmenity(
    Guid id,
    AmenityUpdateRequest amenityUpdateRequest,
    CancellationToken cancellationToken)
  {
    var command = new UpdateAmenityCommand { AmenityId = id };
    mapper.Map(amenityUpdateRequest, command);
    
    await mediator.Send(command, cancellationToken);

    return NoContent();
  }
}