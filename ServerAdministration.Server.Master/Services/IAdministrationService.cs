using System.Collections.Generic;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.Server.Master.Services
{
    public interface IAdministrationService
    {
        IEnumerable<Insurance> GetAllInsurances();
        List<SiteIISLog> GetDataFrom(Insurance insurance);
        void GetDataFromAllInsurances();
    }
}