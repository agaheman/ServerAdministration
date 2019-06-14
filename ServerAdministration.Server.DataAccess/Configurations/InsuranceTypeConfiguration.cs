using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.Server.DataAccess.Configurations
{
    public class InsuranceTypeConfiguration : IEntityTypeConfiguration<Insurance>
    {
        public void Configure(EntityTypeBuilder<Insurance> builder)
        {
            builder.HasKey(p => p.InsuranceId);
            builder.Property(i => i.InsuranceId).ValueGeneratedNever();
        }
    }
}
