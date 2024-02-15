using System.ComponentModel;
using System.Linq.Expressions;
using TABP.Domain.Enums;

namespace TABP.Infrastructure.Persistence.Extensions;

public static class SortingExtensions
{
  public static IQueryable<TItem> Sort<TItem>(
    this IQueryable<TItem> queryable,
    Expression<Func<TItem, object>> sortColumnExpression,
    SortOrder sortOrder)
  {
    return sortOrder switch
    {
      SortOrder.Ascending => queryable.OrderBy(sortColumnExpression),
      SortOrder.Descending => queryable.OrderByDescending(sortColumnExpression),
      _ => throw new InvalidEnumArgumentException()
    };
  }
}