using System.Text.Json;
using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Cities;
using TABP.Api.Dtos.Images;
using TABP.Application.Cities.Create;
using TABP.Application.Cities.Delete;
using TABP.Application.Cities.GetForManagement;
using TABP.Application.Cities.GetTrending;
using TABP.Application.Cities.SetThumbnail;
using TABP.Application.Cities.Update;
using TABP.Domain;
using TABP.Domain.Enums;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/cities")]
[ApiVersion("1.0")]
[Authorize(Roles = UserRoles.Admin)]
public class CitiesController(ISender mediator) : ControllerBase
{
  /// <summary>
  ///   Retrieve a page of cities based on the provided parameters for management (admin).
  /// </summary>
  /// <param name="citiesGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The request page of cities for management, with pagination metadata included.</returns>
  /// <response code="200">Returns the request page of cities for management, with pagination metadata included.</response>
  /// <response code="400">If the request parameters are invalid or missing.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  [ProducesResponseType(typeof(IEnumerable<CityForManagementResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<CityForManagementResponse>>> GetCitiesForManagement(
    [FromQuery] CitiesGetRequest citiesGetRequest,
    CancellationToken cancellationToken)
  {
    var sortOrder = citiesGetRequest.SortOrder switch
    {
      "asc" => SortOrder.Ascending,
      "desc" => SortOrder.Descending,
      _ => (SortOrder?)null
    };

    var query = new GetCitiesForManagementQuery(
      citiesGetRequest.SearchTerm,
      sortOrder,
      citiesGetRequest.SortColumn,
      citiesGetRequest.PageNumber,
      citiesGetRequest.PageSize);

    var owners = await mediator.Send(query, cancellationToken);

    Response.Headers["x-pagination"] = JsonSerializer.Serialize(
      owners.PaginationMetadata);

    return Ok(owners.Items);
  }

  /// <summary>
  ///   Returns TOP N most visited cities (trending cities).
  /// </summary>
  /// <param name="trendingCitiesGetRequest">The number of trending cities to retrieve (N).</param>
  /// <param name="cancellationToken"></param>
  /// <returns>
  ///   The requested N top most trending cities.
  /// </returns>
  /// <response code="200">Returns TOP N most visited cities.</response>
  /// <response code="400">
  ///   If the provided count (N) is less than or equal to zero
  ///   or greater than 100.
  /// </response>
  [ProducesResponseType(typeof(IEnumerable<TrendingCityResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [HttpGet("trending")]
  [AllowAnonymous]
  public async Task<ActionResult<IEnumerable<TrendingCityResponse>>> GetTrendingCities(
    [FromQuery] TrendingCitiesGetRequest trendingCitiesGetRequest,
    CancellationToken cancellationToken)
  {
    var query = new GetTrendingCitiesQuery(trendingCitiesGetRequest.Count);

    var cities = await mediator.Send(query, cancellationToken);

    return Ok(cities);
  }

  /// <summary>
  ///   Create a new city.
  /// </summary>
  /// <param name="cityCreationRequest">The data of the new city.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="201">If the city was created successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="409">If a city with the same post office exists.</response>
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPost]
  public async Task<IActionResult> CreateCity(
    CityCreationRequest cityCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = new CreateCityCommand(
      cityCreationRequest.Name,
      cityCreationRequest.Country,
      cityCreationRequest.PostOffice);

    await mediator.Send(
      command,
      cancellationToken);

    return Created();
  }

  /// <summary>
  ///   Update an existing city specified by ID.
  /// </summary>
  /// <param name="id">The ID of the city.</param>
  /// <param name="cityUpdateRequest">The updated data of the city.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the city was updated successfully.</response>
  /// <response code="400">If the request data is invalid.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the city was not found.</response>
  /// <response code="409">If a city with the same post office exists.</response>
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpPut("{id:guid}")]
  public async Task<IActionResult> UpdateCity(Guid id,
    CityUpdateRequest cityUpdateRequest,
    CancellationToken cancellationToken)
  {
    var command = new UpdateCityCommand(id,
      cityUpdateRequest.Name,
      cityUpdateRequest.Country,
      cityUpdateRequest.PostOffice);

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Delete an existing city specified by ID.
  /// </summary>
  /// <param name="id">The ID of the city.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the city was deleted successfully.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the city was not found.</response>
  /// <response code="409">If there are hotels in the city.</response>
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteCity(
    Guid id,
    CancellationToken cancellationToken)
  {
    var command = new DeleteCityCommand(id);

    await mediator.Send(
      command,
      cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Set the thumbnail of a city specified by ID.
  /// </summary>
  /// <param name="id">The ID of the city</param>
  /// <param name="imageCreationRequest">The new thumbnail</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the thumbnail was set successfully.</response>
  /// <response code="400">If the thumbnail is invalid (not .jpg, .jpeg, or .png).</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  /// <response code="404">If the city specified by ID is not found.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpPut("{id:guid}/thumbnail")]
  public async Task<IActionResult> SetCityThumbnail(
    Guid id,
    [FromForm] ImageCreationRequest imageCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = new SetCityThumbnailCommand(id,
      imageCreationRequest.Image);

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }
}