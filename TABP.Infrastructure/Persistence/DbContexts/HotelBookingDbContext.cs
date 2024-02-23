using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TABP.Domain.Entities;
using TABP.Infrastructure.Persistence.Configurations;

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
    modelBuilder.ApplyConfiguration(new AmenityConfiguration());
    modelBuilder.ApplyConfiguration(new BookingConfiguration());
    modelBuilder.ApplyConfiguration(new CityConfiguration());
    modelBuilder.ApplyConfiguration(new DiscountConfiguration());
    modelBuilder.ApplyConfiguration(new HotelConfiguration());
    modelBuilder.ApplyConfiguration(new ImageConfiguration());
    modelBuilder.ApplyConfiguration(new InvoiceRecordConfiguration());
    modelBuilder.ApplyConfiguration(new OwnerConfiguration());
    modelBuilder.ApplyConfiguration(new ReviewConfiguration());
    modelBuilder.ApplyConfiguration(new RoleConfiguration());
    modelBuilder.ApplyConfiguration(new RoomClassConfiguration());
    modelBuilder.ApplyConfiguration(new RoomConfiguration());
    modelBuilder.ApplyConfiguration(new UserConfiguration());
  }
}