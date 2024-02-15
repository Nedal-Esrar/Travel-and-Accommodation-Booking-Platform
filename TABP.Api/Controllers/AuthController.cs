using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TAABB.Application.Users.Login;
using TABP.Api.Dtos.Auth;
using TABP.Application.Users.Login;
using TABP.Application.Users.Register;
using TABP.Domain;

namespace TABP.Api.Controllers;

[Route("api/auth")]
[ApiController]
[ApiVersion("1.0")]
public class AuthController(ISender mediator) : ControllerBase
{
  /// <summary>
  ///   Processes a login request.
  /// </summary>
  /// <param name="loginRequest">Login request data.</param>
  /// <param name="cancellationToken"></param>
  /// <returns>JWT if the login process was successful.</returns>
  /// <response code="200">Returns JWT token.</response>
  /// <response code="400">If the provided credentials are invalid.</response>
  /// <response code="401">When user with the provided credentials does not exist.</response>
  [HttpPost("login")]
  [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status401Unauthorized)]
  public async Task<ActionResult<LoginResponse>> Login(
    LoginRequest loginRequest,
    CancellationToken cancellationToken)
  {
    var loginCommand = new LoginCommand(
      loginRequest.Email,
      loginRequest.Password);

    return Ok(await mediator.Send(loginCommand, cancellationToken));
  }

  /// <summary>
  ///   Processes registering a guest request.
  /// </summary>
  /// <param name="registerRequest">Registering request data.</param>
  /// <param name="cancellationToken"></param>
  /// <response code="204">If the registration process was successful.</response>
  /// <response code="400">If the register data are invalid.</response>
  /// <response code="409">If a user with the same email is already registered.</response>
  [HttpPost("register-guest")]
  [ProducesResponseType(StatusCodes.Status204NoContent)]
  [ProducesResponseType(StatusCodes.Status400BadRequest)]
  [ProducesResponseType(StatusCodes.Status409Conflict)]
  public async Task<IActionResult> RegisterUser(
    RegisterRequest registerRequest,
    CancellationToken cancellationToken)
  {
    var registerCommand = new RegisterCommand(
      registerRequest.FirstName,
      registerRequest.LastName,
      registerRequest.Email,
      registerRequest.Password,
      UserRoles.Guest);

    await mediator.Send(registerCommand, cancellationToken);

    return NoContent();
  }
}