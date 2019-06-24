using Newtonsoft.Json;
using ServerAdministration.Server.DataAccess.Contracts;
using ServerAdministration.Server.Entities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ServerAdministration.Server.Master.Services
{
    public class AdministrationService : IAdministrationService
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

        public async Task GetDataFromAllInsurancesAsync()
        {
            List<SiteIISLog> siteIISLogs = new List<SiteIISLog>();

            var insurances = GetAllInsurances();

            foreach (var insurance in insurances)
            {
                var serverSitesData = await GetDataFromInsuranceAsync(insurance);
                if (serverSitesData != null)
                {
                    if (serverSitesData.Count == 0)
                        continue;

                    try
                    {
                        siteIISLogs.AddRange(serverSitesData);
                        await insuranceRepository.UpdateAsync(insurance, CancellationToken.None);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
        }
        public void GetDataFromAllInsurances()
        {
            List<SiteIISLog> siteIISLogs = new List<SiteIISLog>();

            var insurances = GetAllInsurances();

            foreach (var insurance in insurances)
            {
                var serverSitesData = GetDataByRestSharpFrom(insurance);
                if (serverSitesData != null)
                {
                    try
                    {
                        siteIISLogs.AddRange(serverSitesData);
                         insuranceRepository.Update(insurance, true);
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }
            }
        }
        public List<SiteIISLog> GetDataByRestSharpFrom(Insurance insurance)
        {
            var apiUrl = $"{insurance.ServerUrl}/api/SiteInfo/GetAllIISLogsAfter/";

            //var requestResult = RestClientHelper.Get<object, List<SiteIISLog>>(apiUrl, new { dateTime = insurance.LastDataGatheringDateTime });

            var client = new RestSharp.RestClient(insurance.ServerUrl);
            var request = new RestSharp.RestRequest("/api/SiteInfo/GetAllIISLogsAfter/", RestSharp.Method.GET);
            request.RequestFormat = RestSharp.DataFormat.Json;
            request.AddBody(new { dateTime = insurance.LastDataGatheringDateTime });
            var requestResult = client.Execute(request);



            if (requestResult.StatusCode == HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<List<SiteIISLog>>(requestResult.Content);

            return null;
        }
        public class LastLogDate
        {
            public DateTime? DateTime { get; set; }
        }
        public async Task<List<SiteIISLog>> GetDataFromInsuranceAsync(Insurance insurance)
        {
            var apiUrl = $"{insurance.ServerUrl}/api/SiteInfo/GetAllIISLogsAfter";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, apiUrl))
            {

                var json = JsonConvert.SerializeObject(new LastLogDate { DateTime = insurance.LastDataGatheringDateTime });

                using (request.Content = new StringContent(json, Encoding.UTF8, "application/json"))
                {

                    using (var response = 
                        await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).ConfigureAwait(false))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();

                            return JsonConvert.DeserializeObject<List<SiteIISLog>>(responseContent);
                        }

                        throw new Exception("Response is not Successfull");
                    }
                }
            }
        }
    }
}