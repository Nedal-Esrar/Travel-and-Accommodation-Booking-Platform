using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;

namespace TABP.Infrastructure.Persistence.DbContexts;

public class HotelBookingDbContext(DbContextOptions<HotelBookingDbContext> options) : DbContext(options)
{
  public DbSet<City> Cities { get; set; }
  public DbSet<Image> Images { get; set; }
  public DbSet<Owner> Owners { get; set; }
  public DbSet<Amenity> Amenities { get; set; }
  public DbSet<Booking> Bookings { get; set; }
  public DbSet<Hotel> Hotels { get; set; }
  public DbSet<Room> Rooms { get; set; }
  public DbSet<RoomClass> RoomClasses { get; set; }
  public DbSet<User> Users { get; set; }
  public DbSet<Role> Roles { get; set; }
  public DbSet<Discount> Discounts { get; set; }
  public DbSet<Review> Reviews { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }
}