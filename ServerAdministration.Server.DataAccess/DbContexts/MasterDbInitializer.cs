using ServerAdministration.Server.Entities;
using System.Linq;

namespace ServerAdministration.Server.DataAccess.DbContexts
{
    public static class MasterDbInitializer
    {
        public static void Initialize(MasterDbContext context)
        {
            context.Database.EnsureCreated();
            if (context.Insurances.Any())
            {
                return;
            }

            var Insurances = new Insurance[]
            {
                new Insurance
                {
                    InsuranceId=10,
                    ServerUrl="www.eppad.com"
                },
                new Insurance
                {
                    InsuranceId=40,
                    ServerUrl="ti.test.eppad.com"
                },

            };

            context.AddRange(Insurances);
            context.SaveChanges();
        }

        public static void SeedNewData(MasterDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Insurances.FirstOrDefault(i => i.InsuranceId == 11) == null)
            {
                context.Insurances.Add(
                    new Insurance
                    {
                        InsuranceId = 11,
                        ServerUrl = "http://transport.iraninsurance.ir"
                    });
            }




            context.SaveChanges();

        }
    }
}


