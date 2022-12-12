using Microsoft.EntityFrameworkCore;
using ThirdEye.Back.DataAccess.Entities;

namespace ThirdEye.Back.DataAccess.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {

        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<InsitutionWorker> Workers { get; set; }
        public DbSet<Institution> Institutions { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomStateChange> RoomsStateHistories { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
