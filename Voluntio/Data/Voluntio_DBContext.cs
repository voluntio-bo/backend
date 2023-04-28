using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voluntio.Data.Entity;

namespace Voluntio.Data
{
    public class Voluntio_DBContext:IdentityDbContext
    {
        public DbSet<EventEntity> Events => Set<EventEntity>();
        public Voluntio_DBContext(DbContextOptions<Voluntio_DBContext> options) : base(options)
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("VoluntioDB");
            optionsBuilder.UseNpgsql(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //events
            modelBuilder.Entity<EventEntity>().ToTable("Event");
            modelBuilder.Entity<EventEntity>().Property(d => d.Id).ValueGeneratedOnAdd();           
        }
    }
}
