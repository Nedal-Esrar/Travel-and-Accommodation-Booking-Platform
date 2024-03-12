using System.Linq.Expressions;
using TABP.Domain.Enums;

namespace TABP.Domain.Models;

public record PaginationQuery<TEntity>(
  Expression<Func<TEntity, bool>> Filter,
  SortOrder SortOrder,
  string? SortColumn,
  int PageNumber,
  int PageSize) : Query<TEntity>(Filter,
  SortColumn,
  SortOrder);