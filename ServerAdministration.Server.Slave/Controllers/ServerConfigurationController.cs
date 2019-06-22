using Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using ServerAdministration.Server.Slave.Configurations;
using ServerAdministration.Server.Slave.Extensions;
using ServerAdministration.WindowOs;

namespace ServerAdministration.Server.Slave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServerConfigurationController : ControllerBase
    {
        private readonly IWritableOptions<ServerConfiguration> serverConfiguratinOption;
        //IWritableOptions<ServerConfiguration> serverConfiguratinOption
        public ServerConfigurationController()
        {
            //this.serverConfiguratinOption = serverConfiguratinOption;
        }

        [HttpPost("[Action]")]
        public void InitializeJson([FromBody]ServerConfiguration serverConfiguration)
        {
            serverConfiguratinOption.Update(
                opt =>
                {
                    opt.ServerId = serverConfiguration.ServerId;
                    opt.ServerName = serverConfiguration.ServerName;
                });
        }

        [HttpPost("[Action]")]
        public void InitializeINI([FromBody]ServerConfiguration serverConfiguration)
        {
            var INISettingFile = new INISettingFile("Settings.ini");

            if (!INISettingFile.KeyExists(nameof(serverConfiguration.ServerId), nameof(ServerConfiguration)))
            {
                INISettingFile.Write(nameof(serverConfiguration.ServerId), serverConfiguration.ServerId.ToString(), nameof(ServerConfiguration));
            }

            if (!INISettingFile.KeyExists(nameof(serverConfiguration.ServerName), nameof(ServerConfiguration)))
            {
                INISettingFile.Write(nameof(serverConfiguration.ServerName), serverConfiguration.ServerName, nameof(ServerConfiguration));
            }
        }
    }
}