using System.Security.Claims;
using System.Text.Json;
using Asp.Versioning;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TABP.Api.Dtos.Bookings;
using TABP.Api.Extensions;
using TABP.Application.Bookings.Common;
using TABP.Application.Bookings.Create;
using TABP.Application.Bookings.Delete;
using TABP.Application.Bookings.GetById;
using TABP.Application.Bookings.GetForGuest;
using TABP.Application.Bookings.GetInvoiceAsPdf;
using TABP.Domain;

namespace TABP.Api.Controllers;

[ApiController]
[Route("api/user/bookings")]
[ApiVersion("1.0")]
[Authorize(Roles = UserRoles.Guest)]
public class BookingsController(ISender mediator, IMapper mapper) : ControllerBase
{
  /// <summary>
  ///   Create a new Booking for the current user.
  /// </summary>
  /// <param name="bookingCreationRequest">New Booking's data.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The newly created booking.</returns>
  /// <response code="201">If the creation process was successful.</response>
  /// <response code="400">
  ///   If the request data is invalid or The provided rooms does not belong to the same
  ///   hotel or one of the rooms is not available in the specified times of check-in and check-out.
  /// </response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an guest).</response>
  /// <response code="404">
  ///   If the hotel specified by ID or one of the provided rooms are not found.
  /// </response>
  [HttpPost]
  [ProducesResponseType(StatusCodes.Status201Created)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  public async Task<IActionResult> CreateBooking(
    BookingCreationRequest bookingCreationRequest,
    CancellationToken cancellationToken)
  {
    var command = mapper.Map<CreateBookingCommand>(bookingCreationRequest);

    var createdBooking = await mediator.Send(command, cancellationToken);

    return CreatedAtAction(nameof(GetBooking), new { id = createdBooking.Id }, createdBooking);
  }

  /// <summary>
  ///   Delete an existing booking specified by ID for the current user.
  /// </summary>
  /// <param name="id">Booking ID.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the deletion process was successful.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an guest).</response>
  /// <response code="404">
  ///   If the booking specified by ID is not found.
  /// </response>
  /// <response code="409">If the booking's check-in date is in the past.</response>
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  [HttpDelete("{id:guid}")]
  public async Task<IActionResult> DeleteBooking(Guid id, CancellationToken cancellationToken)
  {
    var command = new DeleteBookingCommand { BookingId = id };

    await mediator.Send(command, cancellationToken);

    return NoContent();
  }

  /// <summary>
  ///   Get the invoice of a booking specified by ID as PDF for the current user.
  /// </summary>
  /// <param name="id">Booking ID.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested invoice PDF file.</returns>
  /// <response code="200">Returns the requested invoice PDF file.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an guest).</response>
  /// <response code="404">
  ///   If the booking specified by ID is not found.
  /// </response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}/invoice")]
  public async Task<FileResult> GetInvoiceAsPdf(Guid id, CancellationToken cancellationToken)
  {
    var query = new GetInvoiceAsPdfQuery { BookingId = id };

    var pdf = await mediator.Send(query, cancellationToken);

    return File(pdf, "application/pdf", "invoice.pdf");
  }

  /// <summary>
  ///   Get a booking specified by ID for the current user.
  /// </summary>
  /// <param name="id">Booking ID.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested booking specified by ID.</returns>
  /// <response code="200">Returns the requested booking specified by ID.</response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an guest).</response>
  /// <response code="404">
  ///   If the booking specified by ID is not found.
  /// </response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [ProducesResponseType(StatusCodes.Status404NotFound)]
  [HttpGet("{id:guid}")]
  public async Task<ActionResult<BookingResponse>> GetBooking(
    Guid id, CancellationToken cancellationToken)
  {
    var query = new GetBookingByIdQuery { BookingId = id };

    var booking = await mediator.Send(query, cancellationToken);

    return Ok(booking);
  }

  /// <summary>
  ///   Get a page of bookings for the current user based on the provided parameters.
  /// </summary>
  /// <param name="bookingsGetRequest">Request parameters.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>The requested page of bookings, with pagination metadata included.</returns>
  /// <response code="200">
  ///   Return the requested page of bookings, with pagination metadata included.
  /// </response>
  /// <response code="401">User is not authenticated.</response>
  /// <response code="403">User is not authorized (not an guest).</response>
  [ProducesResponseType(StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  [ProducesResponseType(StatusCodes.Status403Forbidden)]
  [HttpGet]
  public async Task<ActionResult<IEnumerable<BookingResponse>>> GetBookings(
    [FromQuery] BookingsGetRequest bookingsGetRequest,
    CancellationToken cancellationToken)
  {
    var query = mapper.Map<GetBookingsQuery>(bookingsGetRequest);

    var bookings = await mediator.Send(query, cancellationToken);

    Response.Headers.AddPaginationMetadata(bookings.PaginationMetadata);

    return Ok(bookings.Items);
  }
}