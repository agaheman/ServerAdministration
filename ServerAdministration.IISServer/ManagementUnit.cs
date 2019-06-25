using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ServerAdministration.Server.DataAccess.Contracts;
using ServerAdministration.Server.Entities;

namespace ServerAdministration.IISServer
{
    public class ManagementUnit
    {
        public static ServerManager ServerManager = new ServerManager();
        private static List<SiteInfo> SitesInfos;
        private static IRepository<SiteIISLog> iISLogRepository;
        public ManagementUnit(IRepository<SiteIISLog> IISLogRepository)
        {
            iISLogRepository = IISLogRepository;
        }
        public static SiteInfo GetSiteInfo(Site site)
        {
            var siteInformation = new SiteInfo
            {
                SiteId = site.Id,
                SiteName = site.Name,
                TraceFailedRequest = new TraceFailedRequest
                {
                    Directory = site.TraceFailedRequestsLogging.Directory,
                    Enabled = site.TraceFailedRequestsLogging.Enabled,
                    MaxLogFiles = site.TraceFailedRequestsLogging.MaxLogFiles
                }
            };

            List<App> applications = new List<App>();

            foreach (var application in site.Applications)
            {
                applications.Add(new App
                {
                    Path = application.Path,
                    ApplicationPoolName = application.ApplicationPoolName,
                    EnabledProtocols = application.EnabledProtocols,
                    EnabledPreload = (bool)application.Attributes["preloadEnabled"].Value
                });
            }

            siteInformation.Applications = applications;

            siteInformation.ServerAutoStart = site.ServerAutoStart;
            siteInformation.LogFile = new LogFile
            {
                Directory = site.LogFile.Directory,
                Enabled = site.LogFile.Enabled,
                LocalTimeRollover = site.LogFile.LocalTimeRollover,
                LogExtFileFlags = site.LogFile.LogExtFileFlags,
                LogFormat = site.LogFile.LogFormat,
                LogTargetW3C = site.LogFile.LogTargetW3C,
                Period = site.LogFile.Period,
                TruncateSize = site.LogFile.TruncateSize,
            };



            List<Binding> bindings = new List<Binding>();
            foreach (var binding in site.Bindings)
            {
                bindings.Add(new Binding
                {
                    BindingInformation = binding.BindingInformation,
                    Host = binding.Host,
                    Protocol = binding.Protocol
                });
            }

            siteInformation.Bindings = bindings;

            return siteInformation;
        }
        public static Site GetSiteBy(string siteName)
        {
            var result = ServerManager.Sites.FirstOrDefault(x=>x.Name==siteName);
            if (result == null)
                throw new Exception("سایتی با این اسم وجود ندارد.");
            
            return result;
        }

        public static List<SiteInfo> GetSitesInfo()
        {
            SitesInfos = new List<SiteInfo>();

            foreach (var site in ServerManager.Sites)
            {
                SiteInfo siteInformation = GetSiteInfoFrom(site);

                SitesInfos.Add(siteInformation);

            }

            return SitesInfos;
        }

        private static SiteInfo GetSiteInfoFrom(Site site)
        {
            var siteInformation = new SiteInfo
            {
                SiteId = site.Id,
                SiteName = site.Name,

                TraceFailedRequest = new TraceFailedRequest
                {
                    Directory = site.TraceFailedRequestsLogging.Directory,
                    Enabled = site.TraceFailedRequestsLogging.Enabled,
                    MaxLogFiles = site.TraceFailedRequestsLogging.MaxLogFiles
                }
            };

            List<App> applications = new List<App>();

            foreach (var application in site.Applications)
            {
                applications.Add(new App
                {
                    Path = application.Path,
                    ApplicationPoolName = application.ApplicationPoolName,
                    EnabledProtocols = application.EnabledProtocols,
                    EnabledPreload = (bool)application.Attributes["preloadEnabled"].Value
                });
            }

            siteInformation.Applications = applications;

            siteInformation.ServerAutoStart = site.ServerAutoStart;
            siteInformation.LogFile = new LogFile
            {
                Directory = site.LogFile.Directory,
                Enabled = site.LogFile.Enabled,
                LocalTimeRollover = site.LogFile.LocalTimeRollover,
                LogExtFileFlags = site.LogFile.LogExtFileFlags,
                LogFormat = site.LogFile.LogFormat,
                LogTargetW3C = site.LogFile.LogTargetW3C,
                Period = site.LogFile.Period,
                TruncateSize = site.LogFile.TruncateSize,
            };



            List<Binding> bindings = new List<Binding>();
            foreach (var binding in site.Bindings)
            {
                bindings.Add(new Binding
                {
                    BindingInformation = binding.BindingInformation,
                    Host = binding.Host,
                    Protocol = binding.Protocol
                });
            }

            siteInformation.Bindings = bindings;
            return siteInformation;
        }

        public const string RequestFilteringSectionName = "system.webServer/security/requestFiltering";

        public static ConfigurationElementCollection GetSiteDeniedUrls(string siteName)
        {
            Configuration config = ServerManager.GetWebConfiguration(siteName);
            ConfigurationSection requestFilteringSection = config.GetSection(RequestFilteringSectionName);
            ConfigurationElementCollection denyQueryStringSequencesCollection = requestFilteringSection.GetCollection("denyUrlSequences");
            return denyQueryStringSequencesCollection;
        }


        public static string GetLogDirectory(Site site)
        {
            string defaultLogDirectory = @"%SystemDrive%\inetpub\logs\LogFiles";

            if (site.LogFile.Enabled)
            {
                if (site.LogFile.Directory == defaultLogDirectory)
                {
                    string unmanagedLogFolder = "W3SVC" + site.Id.ToString();
                    return defaultLogDirectory + "\\" + unmanagedLogFolder;
                }
                return site.LogFile.Directory;
            }
            else return null;
        }
        public List<string> GetLogFiles(Site site)
        {
            List<string> result = new List<string>();

            DirectoryInfo logDirectory = new DirectoryInfo(GetLogDirectory(site));

            return logDirectory.GetFiles("*.log").Select(f => f.FullName).ToList();
        }

        public static List<string> GetLogFilesNewerThan(DateTime dateTime, Site site)
        {
            List<string> result = new List<string>();

            DirectoryInfo logDirectory = new DirectoryInfo(GetLogDirectory(site));

            result = logDirectory.GetFiles("*.log")
                .Where(f => f.LastWriteTime > dateTime)
                .Select(f => f.FullName).ToList();

            return result;
        }

        public static List<string> GetLogFilesNewerThan(string logDirectoryPath)
        {

            DirectoryInfo logDirectory = new DirectoryInfo(logDirectoryPath);

            return logDirectory.GetFiles("*.log")
                .Where(f => f.LastWriteTime > DateTime.Now.AddHours(-7))
                .Select(f => f.FullName).ToList();
        }

        public static SiteInfo GetSiteByLogPath(string filePath)
        {
            //GetSiteBy()
            for (int i = 0; i < ServerManager.Sites.Count; i++)
            {
                if (ServerManager.Sites[i].LogFile.Directory == Path.GetDirectoryName(filePath))
                {
                    return GetSiteInfoFrom(ServerManager.Sites[i]);
                }
            }

            throw new Exception("Site of this log file does not found.");
        }
        public static void SaveParsedLog(FileInfo logFileInfo)
        {
            SiteInfo siteInfo = GetSiteByLogPath(logFileInfo.FullName);
            LogParser logParser = new LogParser();

            var logEvents = logParser.ParseIISLogs(logFileInfo);

            iISLogRepository.AddRange(Map(siteInfo.SiteName, logFileInfo.LastWriteTime, logEvents),true);
        }

        public static IISLogEvent Map(IISLogParser.IISLogEvent iISLogEvent)
        {
            return new IISLogEvent
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
        public static List<SiteIISLog> Map(string siteAppPath, DateTime lastWriteTime, IEnumerable<IISLogParser.IISLogEvent> iISLogEvents)
        {
            List<SiteIISLog> result = new List<SiteIISLog>();

            foreach (var iISLogEvent in iISLogEvents)
            {
                result.Add(new SiteIISLog
                {
                    LastDateModified = lastWriteTime,
                    SiteAppPath = siteAppPath,
                    SlaveServerId = 0,
                    IISLogEvent = Map(iISLogEvent)
                });
            }
            return result;
        }
    }
}
