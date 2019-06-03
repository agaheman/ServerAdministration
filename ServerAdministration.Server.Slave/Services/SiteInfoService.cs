using IISLogParser;
using ServerAdministration.IISServer;
using ServerAdministration.Server.DataAccess.Contracts;
using ServerAdministration.Server.Entities;
using System;
using System.Collections.Generic;

namespace ServerAdministration.Server.Slave.Services
{
    public class SiteInfoService : ISiteInfoService
    {
        private readonly IRepository<SiteIISLog> IISLogRepository;

        public SiteInfoService(IRepository<SiteIISLog> IISLogRepository)
        {
            this.IISLogRepository = IISLogRepository;
        }

        public void SaveSiteInfoRange(List<SiteIISLog> siteIISLogs)
        {
            IISLogRepository.AddRange(siteIISLogs, true);
        }
        public void SaveSiteInfo(SiteIISLog siteIISLog)
        {
            IISLogRepository.Add(siteIISLog, true);
        }

        public Entities.IISLogEvent Map(IISLogParser.IISLogEvent iISLogEvent)
        {
            return new Entities.IISLogEvent
            {
                ClientIp = iISLogEvent.cIp,
                Computername = iISLogEvent.sComputername,
                Cookie = iISLogEvent.csCookie,
                DateTimeEvent = iISLogEvent.DateTimeEvent,
                Host = iISLogEvent.csHost,
                Method = iISLogEvent.csMethod,
                ProtocolStatus = iISLogEvent.scStatus,
                ProtocolSubstatus = iISLogEvent.scSubstatus,
                ProtocolVersion = iISLogEvent.csVersion,
                SentBytes = iISLogEvent.scBytes,
                RecievedBytes = iISLogEvent.csBytes,
                Win32Status = iISLogEvent.scWin32Status,
                Referer = iISLogEvent.csReferer,
                ServerIp = iISLogEvent.sIp,
                ServerPort = iISLogEvent.sPort,
                Sitename = iISLogEvent.sSitename,
                TimeTaken = iISLogEvent.timeTaken,
                UriQuery = iISLogEvent.csUriQuery,
                UriStem = iISLogEvent.csUriStem,
                UserAgent = iISLogEvent.csUserAgent,
                Username = iISLogEvent.csUsername
            };
        }


        public List<SiteIISLog> Map(string siteAppPath, IEnumerable<IISLogParser.IISLogEvent> iISLogEvents)
        {
            List<SiteIISLog> result = new List<SiteIISLog>();
            foreach (var iISLogEvent in iISLogEvents)
            {
                result.Add(new SiteIISLog
                {
                    SiteAppPath = siteAppPath,
                    SlaveServerId = 0,
                    IISLogEvent = Map(iISLogEvent)
                });
            }
            return result;
        }


    }
}
