using System.Linq.Expressions;
using LinqToDB.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Domain.Enums;
using TABP.Domain.Exceptions;
using TABP.Domain.Interfaces.Persistence.Repositories;
using TABP.Domain.Messages;
using TABP.Domain.Models;
using TABP.Infrastructure.Persistence.DbContexts;
using TABP.Infrastructure.Persistence.Extensions;
using TABP.Infrastructure.Persistence.Helpers;

namespace TABP.Infrastructure.Persistence.Repositories;

public class BookingRepository(HotelBookingDbContext context) : IBookingRepository
{
  public async Task<bool> ExistsAsync(Expression<Func<Booking, bool>> predicate,
                                      CancellationToken cancellationToken = default)
  {
    return await context.Bookings.AnyAsync(predicate, cancellationToken);
  }

  public async Task<Booking> CreateAsync(Booking booking, CancellationToken cancellationToken = default)
  {
    ArgumentNullException.ThrowIfNull(booking);

    var createdBooking = await context.AddAsync(booking, cancellationToken);

    return createdBooking.Entity;
  }

  public async Task<Booking?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
  {
    return await context.Bookings
      .FindAsync([id], cancellationToken);
  }

  public async Task<Booking?> GetByIdAsync(Guid id, Guid guestId,
    bool includeInvoice = false, 
    CancellationToken cancellationToken = default)
  {
    var bookings = context.Bookings
      .Where(b => b.Id == id && b.GuestId == guestId)
      .Include(b => b.Hotel);

    if (includeInvoice)
    {
      bookings.Include(b => b.Invoice);
    }

    return await bookings.FirstOrDefaultAsync(cancellationToken);
  }

  public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
  {
    if (!await context.Bookings.AnyAsync(b => b.Id == id, cancellationToken))
    {
      throw new NotFoundException(BookingMessages.NotFound);
    }
    
    var entity = context.ChangeTracker.Entries<Booking>()
                   .FirstOrDefault(e => e.Entity.Id == id)?.Entity
                 ?? new Booking { Id = id };

    context.Bookings.Remove(entity);
  }
  public async Task<PaginatedList<Booking>> GetAsync(Query<Booking> query,
    CancellationToken cancellationToken = default)
  {
    var queryable = context.Bookings
      .Where(query.Filter)
      .Sort(SortingExpressions.GetBookingSortExpression(query.SortColumn), query.SortOrder);

    var itemsToReturn = await queryable
      .GetPage(query.PageNumber, query.PageSize)
      .AsNoTracking()
      .ToListAsync(cancellationToken);

    return new PaginatedList<Booking>(
      itemsToReturn,
      await queryable.GetPaginationMetadataAsync(
        query.PageNumber,
        query.PageSize));
  }

  public async Task<IEnumerable<Booking>> GetRecentBookingsInDifferentHotelsByGuestId(Guid guestId, 
    int count, CancellationToken cancellationToken = default)
  {
    var recentBookingsQuery = 
      from b in (
        from br in 
          from b in context.Bookings.ToLinqToDB()
          where b.GuestId == guestId
          select new
          {
            Booking = b,
            Rank = LinqToDB.AnalyticFunctions
              .RowNumber(LinqToDB.Sql.Ext)
              .Over()
              .PartitionBy(b.HotelId)
              .OrderByDesc(b.CheckInDateUtc)
              .ToValue()
          }
        where br.Rank <= 1
        select br.Booking).Take(count)
      join h in context.Hotels.Include(h => h.City).ToLinqToDB()
        on b.HotelId equals h.Id
      join i in context.Images.ToLinqToDB()
        on h.Id equals i.EntityId into images
      from img in images.DefaultIfEmpty()
      where img.Type == ImageType.Thumbnail
      select new { Booking = b, Thumbnail = img };

    var recentBookings = await recentBookingsQuery
      .AsNoTracking()
      .ToListAsync(cancellationToken);

    return recentBookings.Select(b =>
    {
      b.Booking.Hotel.Thumbnail = b.Thumbnail;

      return b.Booking;
    });
  }
}