using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.Server.DataAccess.Configurations
{
    public class SiteIISLogTypeConfiguration : IEntityTypeConfiguration<SiteIISLog>
    {
        public void Configure(EntityTypeBuilder<SiteIISLog> builder)
        {
            builder.HasKey(p => p.Id);
        }
    }
}
