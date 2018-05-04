//using EventMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPromo.Data
{
    public class EventDBContext : DbContext
    {
        public DbSet<Event> Event { get; set; }
        public DbSet<EventCategory> Categories { get; set; }

        public DbSet<Lookup> Lookup { get; set; }
        public DbSet<Lookup> Lookup { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lookup>()
                .HasKey(c => { c.CheeseID, c.MenuID });
        }

        public EventDBContext(DbContextOptions<EventDbContext> options)
            : base(options)
        { }
    }
}
