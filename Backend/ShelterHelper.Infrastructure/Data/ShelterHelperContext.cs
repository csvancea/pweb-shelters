using ShelterHelper.Core.DataModel;
using ShelterHelper.Infrastructure.Data.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace ShelterHelper.Infrastructure.Data
{
    public class ShelterHelperContext : DbContext
    {
        public ShelterHelperContext(DbContextOptions<ShelterHelperContext> options) : base(options) 
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SheltersConfiguration).Assembly);
        }

        public DbSet<Users> Users => Set<Users>();
        public DbSet<Shelters> Shelters => Set<Shelters>();
        public DbSet<Bookings> Bookings => Set<Bookings>();
    }
}
