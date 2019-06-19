using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace ServerAdministration.Server.Slave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerConfigurationController : ControllerBase
    {
        private readonly IConfiguration configuration;

        public ServerConfigurationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        [HttpPost("[Action]")]
        public void Initialize(int insuranceId)
        {
            var asd = configuration.GetSection("InsuranceId");
            AppSettings.AddOrUpdate("InsuranceId", insuranceId.ToString());
        }
    }
}