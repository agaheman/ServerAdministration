using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using ServerAdministration.Server.Slave.Configurations;
using ServerAdministration.Server.Slave.Extensions;

namespace ServerAdministration.Server.Slave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerConfigurationController : ControllerBase
    {
        private readonly IWritableOptions<ServerConfiguration> serverConfiguratinOption;

        public ServerConfigurationController(IWritableOptions<ServerConfiguration> serverConfiguratinOption)
        {
            this.serverConfiguratinOption = serverConfiguratinOption;
        }
        [HttpPost("[Action]")]
        public void Initialize([FromBody]ServerConfiguration serverConfiguration)
        {
            serverConfiguratinOption.Update(
                opt =>
                {
                    opt.ServerId = serverConfiguration.ServerId;
                    opt.ServerName = serverConfiguration.ServerName;
                });
        }
    }
}