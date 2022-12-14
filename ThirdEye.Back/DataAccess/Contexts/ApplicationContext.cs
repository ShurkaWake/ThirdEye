using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ThirdEye.Back.DataAccess.Entities;

namespace ThirdEye.Back.DataAccess.Contexts
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) 
            : base(options)
        {
            
        }

        public DbSet<Device> Devices { get; set; }
        public DbSet<BusinessWorker> Workers { get; set; }
        public DbSet<Business> Businesses { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomStateRecord> RoomsStateHistories { get; set; }
    }
}
