using Microsoft.EntityFrameworkCore;
using ServerAdministration.IISServer;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.Server.DataAccess.DbContexts
{
    public class SlaveServerDbContext : DbContext
    {
        //DbSet<SiteInfo> Sites { get; set; }
        DbSet<IISLogEvent> IISLogEvents { get; set; }
        DbSet<SiteIISLog> SiteIISLogs { get; set; }


        public SlaveServerDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(IISLogEvent));
            modelBuilder.Entity(typeof(SiteIISLog));

            modelBuilder.ApplyConfiguration(new SiteIISLogConfiguration());
        }
    }
}
