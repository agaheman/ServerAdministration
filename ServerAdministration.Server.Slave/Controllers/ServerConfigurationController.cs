﻿using Common.Utilities;
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
        public ServerConfigurationController()
        {
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