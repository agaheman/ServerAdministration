using Microsoft.EntityFrameworkCore;
using ServerAdministration.Server.Entities;
using System.Linq;

namespace ServerAdministration.Server.DataAccess.DbContexts
{
    public static class DbInitializer
    {
        public static void InitializeMasterDb(MasterDbContext context)
        {
            context.Database.Migrate();
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

        public static void MasterDbSeedNewData(MasterDbContext context)
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

        public static void InitializeSlaveDb(SlaveDbContext slaveDbContext)
        {
            slaveDbContext.Database.Migrate();
            //if (slaveDbContext.Insurances.Any())
            //{
            //    return;
            //}

            //var Insurances = new Insurance[]
            //{
            //    new Insurance
            //    {
            //        InsuranceId=10,
            //        ServerUrl="www.eppad.com"
            //    },
            //    new Insurance
            //    {
            //        InsuranceId=40,
            //        ServerUrl="ti.test.eppad.com"
            //    },

            //};

            //slaveDbContext.AddRange(Insurances);
            //slaveDbContext.SaveChanges();
        }

        public static void SlaveDbSeedNewData(SlaveDbContext slaveDbContext)
        {
            //slaveDbContext.Database.EnsureCreated();

            //if (slaveDbContext.Insurances.FirstOrDefault(i => i.InsuranceId == 11) == null)
            //{
            //    slaveDbContext.Insurances.Add(
            //        new Insurance
            //        {
            //            InsuranceId = 11,
            //            ServerUrl = "http://transport.iraninsurance.ir"
            //        });
            //}




            //slaveDbContext.SaveChanges();

        }
    }
}


