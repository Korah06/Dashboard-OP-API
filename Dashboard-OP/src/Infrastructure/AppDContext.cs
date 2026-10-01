using Dashboard_OP.src.api.Models;
using Microsoft.EntityFrameworkCore;

namespace Dashboard_OP.src.Infrastructure
{
    public class AppDbContext : DbContext
    {

        protected readonly IConfiguration Configuration;
        public AppDbContext(IConfiguration configuration) { 
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection from app settings
            optionsBuilder.UseNpgsql(Configuration.GetConnectionString("ApiDatabase"));
        }

        public DbSet<User> Users { get; set; }
        public DbSet<InfoLog> InfoLogs { get; set; }

    }
}
