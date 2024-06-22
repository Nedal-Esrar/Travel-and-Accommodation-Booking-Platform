using System.Linq.Expressions;
using TABP.Domain.Enums;

namespace TABP.Domain.Models;

public record Query<TEntity>(
  Expression<Func<TEntity, bool>> Filter,
  SortOrder SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize);