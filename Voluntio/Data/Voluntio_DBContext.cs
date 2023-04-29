using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Voluntio.Data.Entity;

namespace Voluntio.Data
{
    public class Voluntio_DBContext:IdentityDbContext
    {
        public DbSet<EventEntity> Events => Set<EventEntity>();
        public DbSet<OrganizationEntity> Organizations => Set<OrganizationEntity>();
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
            modelBuilder.Entity<EventEntity>().HasOne(w => w.Organization).WithMany(i => i.Events);

            //organizations
            modelBuilder.Entity<OrganizationEntity>().ToTable("Organizations");
            modelBuilder.Entity<OrganizationEntity>().Property(o =>  o.Id).ValueGeneratedOnAdd();
            modelBuilder.Entity<OrganizationEntity>().HasMany(o => o.Events).WithOne(e => e.Organization);
        }
    }
}
