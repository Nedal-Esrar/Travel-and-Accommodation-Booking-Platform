using Microsoft.EntityFrameworkCore;
using TABP.Domain.Models;

namespace TABP.Infrastructure.Persistence.Extensions;

public static class PaginationExtensions
{
  public static IQueryable<TItem> GetPage<TItem>(
    this IQueryable<TItem> queryable,
    int pageNumber, int pageSize)
  {
    return queryable.Skip(pageSize * (pageNumber - 1))
      .Take(pageSize);
  }

  public static async Task<PaginationMetadata> GetPaginationMetadataAsync<TItem>(
    this IQueryable<TItem> queryable,
    int pageNumber, int pageSize)
  {
    return new PaginationMetadata(
      await queryable.CountAsync(),
      pageNumber,
      pageSize);
  }
}