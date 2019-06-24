using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Web.Administration;
using ServerAdministration.IISServer;
using ServerAdministration.Server.Entities;
using ServerAdministration.Server.Slave.Services;
using ServerAdministration.WindowOs;
using System;
using System.Collections.Generic;
using System.IO;

namespace ServerAdministration.Server.Slave.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteInfoController : ControllerBase
    {
        private readonly ISiteInfoService siteInfoService;

        public SiteInfoController(ISiteInfoService siteInfoService)
        {
            this.siteInfoService = siteInfoService;
        }

        public class LastLogDate
        {
            public DateTime? DateTime { get; set; }
        }
        [HttpGet("[Action]")]
        public List<SiteIISLog> GetAllIISLogsAfter(LastLogDate logDate)
        {
            var result = siteInfoService.GetAllIISLogsAfter(logDate.DateTime);

            return result;
        }



        [HttpGet("[Action]")]
        public List<SiteInfo> GetSitesInfo()
        {
            var result = ManagementUnit.GetSitesInfo();
            return result;
        }

        [HttpGet("ParseLogFiles")]
        public IEnumerable<IISLogParser.IISLogEvent> ParseLogFiles([FromQuery] string siteName)
        {
            var logFiles = ManagementUnit.GetLogFilesNewerThan(@"C:\Agah\MySiteFiles\Log");

            LogParser logParser = new LogParser();
            List<IISLogParser.IISLogEvent> iISLogEvents = new List<IISLogParser.IISLogEvent>();

            foreach (var logfile in logFiles)
            {
                iISLogEvents.AddRange(logParser.ParseIISLogs(logfile));
            };

            return iISLogEvents;
        }

        [HttpGet("[action]")]
        public void SaveParsedFiles([FromQuery] string siteName)
        {
            var site = ManagementUnit.GetSiteBy(siteName);
            var logFilePaths = ManagementUnit.GetLogFilesNewerThan(DateTime.Now.AddHours(-6), site);

            LogParser logParser = new LogParser();
            List<SiteIISLog> iISLogEvents = new List<SiteIISLog>();

            foreach (var logfilePath in logFilePaths)
            {
                var logEvents = logParser.ParseIISLogs(logfilePath);
                var lastWriteTime = System.IO.File.GetLastWriteTime(logfilePath);
                iISLogEvents.AddRange(siteInfoService.Map(site.Name, lastWriteTime, logEvents));
            };

            siteInfoService.SaveSiteInfoRange(iISLogEvents);

        }

        [HttpGet("GetSiteInfo")]
        public SiteInfo GetSiteInfoBy([FromQuery] string siteName)
        {
            var site = ManagementUnit.GetSiteBy(siteName);
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
