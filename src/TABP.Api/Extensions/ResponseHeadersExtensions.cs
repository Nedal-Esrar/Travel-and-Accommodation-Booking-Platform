using System.Text.Json;
using TABP.Domain.Models;

namespace TABP.Api.Extensions;

public static class ResponseHeadersExtensions
{
  public static void AddPaginationMetadata(this IHeaderDictionary headers,
    PaginationMetadata paginationMetadata)
  {
    headers["x-pagination"] = JsonSerializer.Serialize(paginationMetadata);
  }
}