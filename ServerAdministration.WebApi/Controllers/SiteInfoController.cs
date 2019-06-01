using Common;
using IISLogParser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Administration;
using ServerAdministration.IISServer;
using System;
using System.Collections.Generic;

namespace ServerAdministration.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteInfoController : ControllerBase
    {
        [HttpGet("[Action]")]
        public List<SiteInfo> GetSitesInfo()
        {
            var result = ManagementUnit.GetSitesInfo();
            return result;
        }

        [HttpGet("ParseLogFiles")]
        public List<IISLogEvent> ParseLogFiles([FromQuery] string siteName)
        {
            var logFiles = ManagementUnit.GetLogFilesNewerThan(@"C:\Agah\MySiteFiles\Log");

            LogParser logParser = new LogParser();
            List<IISLogEvent> iISLogEvents = new List<IISLogEvent>();

            foreach (var logfile in logFiles)
            {
                iISLogEvents.AddRange(logParser.ParseIISLogs(logfile));
            };


            return iISLogEvents;
        }


        [HttpGet("GetSiteInfo")]
        public SiteInfo GetSiteInfoBy([FromQuery] string logDirectory)
        {
            var site = ManagementUnit.GetSiteBy(logDirectory);
            var siteInfo = ManagementUnit.GetSiteInfo(site);
            return siteInfo;
        }

        [AllowAnonymous]
        [HttpGet("LogFiles")]
        public string LogFiles([FromQuery] string siteName)
        {
            Site site = ManagementUnit.GetSiteBy(siteName);
            if (site == null) throw new Exception("SiteNotFound");

            var LogFilesNewerThan = ManagementUnit.GetLogFilesNewerThan(DateTime.Now.AddHours(-5), site);
            var ZippedLogFilesPath = ZipHelper.ZipLogFiles(LogFilesNewerThan);

            return ZippedLogFilesPath;
        }
    }
}
