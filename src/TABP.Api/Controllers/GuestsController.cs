using System.Security.Claims;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Hotels;
using TABP.Application.Hotels.GetRecentlyVisited;
using TABP.Domain;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/user")]
[ApiVersion("1.0")]
[Authorize(Roles = UserRoles.Guest)]
public class GuestsController(ISender mediator, IMapper mapper) : ControllerBase
{
  /// <summary>
  ///   Retrieve the recently N visited hotels by the current user.
  /// </summary>
  /// <param name="recentlyVisitedHotelsGetRequest">The number of the requested visited hotels (N).</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested N recently visited hotels by the current user.</returns>
  /// <response code="200">Returns The requested N recently visited hotels by the current user.</response>
  /// <response code="400">
  ///   If the provided count is less than or equal to zero or greater than 100.
  /// </response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an admin).</response>
  [ProducesResponseType(typeof(IEnumerable<RecentlyVisitedHotelResponse>), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpGet("recently-visited-hotels")]
  public async Task<ActionResult<IEnumerable<RecentlyVisitedHotelResponse>>> GetRecentlyVisitedHotels(
    [FromQuery] RecentlyVisitedHotelsGetRequest recentlyVisitedHotelsGetRequest,
    CancellationToken cancellationToken)
  {
    var guestId = Guid.Parse(
      User.FindFirstValue(ClaimTypes.NameIdentifier)
      ?? throw new ArgumentNullException());

    var query = new GetRecentlyVisitedHotelsForGuestQuery { GuestId = guestId };
    mapper.Map(recentlyVisitedHotelsGetRequest, query);

    var hotels = await mediator.Send(query, cancellationToken);

    return Ok(hotels);
  }
}