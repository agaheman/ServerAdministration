using ServerAdministration.Server.DataAccess.Contracts;
using ServerAdministration.Server.Entities;
using System;
using System.Collections.Generic;
using System.Net;

namespace ServerAdministration.Server.Master.Services
{
    public class AdministrationService:IAdministrationService
    {
        private readonly IRepository<Insurance> insuranceRepository;
        public AdministrationService(IRepository<Insurance> insuranceRepository)
        {
            this.insuranceRepository = insuranceRepository;
        }
        public IEnumerable<Insurance> GetAllInsurances()
        {
            return insuranceRepository.TableNoTracking;
        }

        public void GetDataFromAllInsurances()
        {
            List<SiteIISLog> siteIISLogs = new List<SiteIISLog>();

            var insurances = GetAllInsurances();

            foreach (var insurance in insurances)
            {
                var serverSitesData = GetDataFrom(insurance);

                siteIISLogs.AddRange(serverSitesData);


                try
                {

                    insuranceRepository.Update(insurance, true);
                }
                catch (System.Exception)
                {
                    continue;
                }
            }
        }
        public List<SiteIISLog> GetDataFrom(Insurance insurance)
        {
            var apiUrl = $"{insurance.ServerUrl}/api/SiteInfo/GetAllIISLogsAfter";
            //var result = RestClientHelper.Post<PayeshgaranRequestDto, PayeshgaranResponseDto>(webServiceAddress, requestedData);
            var requestResult = RestClientHelper.Get<DateTime, List<SiteIISLog>>(apiUrl, insurance.LastDataGatheringDateTime??DateTime.MinValue);

            if (requestResult.StatusCode == HttpStatusCode.OK)
                return requestResult.Data;

            return null;
        }
    }
}