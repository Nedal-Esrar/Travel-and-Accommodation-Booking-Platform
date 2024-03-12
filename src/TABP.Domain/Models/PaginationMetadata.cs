namespace TABP.Domain.Models;

public record PaginationMetadata(
  int TotalItemCount,
  int CurrentPage,
  int PageSize)
{
  public int TotalPageCount => (int)Math.Ceiling((double)TotalItemCount / PageSize);
}