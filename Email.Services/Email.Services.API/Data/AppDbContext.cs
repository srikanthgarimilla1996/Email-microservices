using Email.Services.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.Services.API.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User { Id = 1, UserName = "srikanthg", Email = "srikanthg@technoflair.com" },
                new User { Id = 2, UserName = "revanthv", Email = "revanthv@technoflair.com"},
                new User { Id = 3, UserName = "sreedhars", Email = "sreedhars@technoflair.com"},
                new User { Id = 4, UserName = "mohsinm", Email = "mohsinm@technoflair.com"},
                new User { Id = 5, UserName = "akhilas", Email = "akhilas@technoflair.com"},
                new User { Id = 6, UserName = "ravichandrav", Email = "ravichandrav@technoflair.com" }
                );
        }
    }
}
