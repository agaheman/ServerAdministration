using Microsoft.EntityFrameworkCore;
using ServerAdministration.Server.DataAccess.Configurations;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.Server.DataAccess.DbContexts
{
    public class MasterDbContext : DbContext
    {
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<SiteIISLog> SiteIISLogs { get; set; }
        public MasterDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InsuranceTypeConfiguration());
        }
    }
}
