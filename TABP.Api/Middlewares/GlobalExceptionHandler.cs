using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using TABP.Domain.Exceptions;

namespace TABP.Api.Middlewares;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
  public async ValueTask<bool> TryHandleAsync(
    HttpContext httpContext, Exception exception,
    CancellationToken cancellationToken)
  {
    LogException(exception);

    var (statusCode, title, detail) = MapExceptionToProblemInformation(exception);

    await Results.Problem(
      statusCode: statusCode,
      title: title,
      detail: detail,
      extensions: new Dictionary<string, object?>
      {
        ["traceId"] = Activity.Current?.Id ?? httpContext.TraceIdentifier
      }).ExecuteAsync(httpContext);

    return true;
  }

  private void LogException(Exception exception)
  {
    if (exception is CustomException)
    {
      logger.LogWarning(exception, exception.Message);
    }
    else
    {
      logger.LogError(exception, exception.Message);
    }
  }

  private static (int statusCode, string title, string detail)
    MapExceptionToProblemInformation(Exception exception)
  {
    if (exception is not CustomException customException)
    {
      return (
        StatusCodes.Status500InternalServerError,
        "Internal server error",
        "Some internal error on the server occured."
      );
    }

    return (
      customException switch
      {
        NotFoundException => StatusCodes.Status404NotFound,
        ConflictException => StatusCodes.Status409Conflict,
        UnauthorizedException => StatusCodes.Status401Unauthorized,
        BadRequestException => StatusCodes.Status400BadRequest,
        _ => StatusCodes.Status500InternalServerError
      },
      customException.Title,
      customException.Message
    );
  }
}