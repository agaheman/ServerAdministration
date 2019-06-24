using System.Collections.Generic;
using System.Threading.Tasks;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.Server.Master.Services
{
    public interface IAdministrationService
    {
        IEnumerable<Insurance> GetAllInsurances();
        List<SiteIISLog> GetDataByRestSharpFrom(Insurance insurance);
        Task GetDataFromAllInsurancesAsync();
        void GetDataFromAllInsurances();
        Task<List<SiteIISLog>> GetDataFromInsuranceAsync(Insurance insurance);
    }
}