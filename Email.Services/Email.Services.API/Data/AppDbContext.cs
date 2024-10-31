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
    }
}
