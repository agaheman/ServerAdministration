using ServerAdministration.Server.DataAccess.Contracts;
using ServerAdministration.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerAdministration.Server.Master.Services
{
    public class InsuranceService
    {
        private readonly IRepository<Insurance> insuranceReository;

        public InsuranceService(IRepository<Insurance> insuranceReository)
        {
            this.insuranceReository = insuranceReository;
        }

        
    }
}
