using Microsoft.EntityFrameworkCore;
using ServerAdministration.Server.DataAccess.Configurations;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.Server.DataAccess.DbContexts
{
    public class SlaveDbContext : DbContext,IDbContext
    {
        //DbSet<SiteInfo> Sites { get; set; }
        DbSet<IISLogEvent> IISLogEvents { get; set; }
        DbSet<SiteIISLog> SiteIISLogs { get; set; }


        public SlaveDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity(typeof(IISLogEvent));
            modelBuilder.Entity(typeof(SiteIISLog));

            //modelBuilder.ApplyConfiguration(new SiteIISLogConfiguration());
        }
    }

    internal interface IDbContext
    {
    }
}
