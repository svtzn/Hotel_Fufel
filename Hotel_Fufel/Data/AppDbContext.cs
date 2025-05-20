using System.Data.Entity;
using Hotel_Fufel;

namespace Hotel_Fufel.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=HotelFufelDb") { }

        public DbSet<User> Users { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
