using Microsoft.EntityFrameworkCore;

namespace SP18.PF.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new VenueConfiguration());
            modelBuilder.ApplyConfiguration(new TourConfiguration());
            modelBuilder.ApplyConfiguration(new EventConfiguration());
            modelBuilder.ApplyConfiguration(new TicketConfiguration());
        }
    }
}
