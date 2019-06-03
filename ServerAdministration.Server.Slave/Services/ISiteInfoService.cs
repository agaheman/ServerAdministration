using System.Collections.Generic;
using IISLogParser;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.Server.Slave.Services
{
    public interface ISiteInfoService
    {
        Entities.IISLogEvent Map(IISLogParser.IISLogEvent iISLogEvent);
        List<SiteIISLog> Map(string siteAppPath, IEnumerable<IISLogParser.IISLogEvent> iISLogEvents);
        void SaveSiteInfo(SiteIISLog siteIISLog);
        void SaveSiteInfoRange(List<SiteIISLog> siteIISLogs);
    }
}