using Email.Services.Processor.Models;
using Microsoft.EntityFrameworkCore;

namespace Email.Services.Processor.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext>options):base(options)
        {

        }

        public DbSet<Logs> Logs { get; set; }
    }
}
