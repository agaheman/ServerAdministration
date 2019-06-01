using Microsoft.Web.Administration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ServerAdministration.IISServer
{
    public class ManagementUnit
    {
        public static ServerManager ServerManager = new ServerManager();
        private static List<SiteInfo> SitesInfo;

        public static SiteInfo GetSiteInfo(Site site)
        {
            var siteInformation = new SiteInfo();

            siteInformation.Id = site.Id;
            siteInformation.Name = site.Name;
            siteInformation.TraceFailedRequest = new TraceFailedRequest
            {
                Directory = site.TraceFailedRequestsLogging.Directory,
                Enabled = site.TraceFailedRequestsLogging.Enabled,
                MaxLogFiles = site.TraceFailedRequestsLogging.MaxLogFiles
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
            var result = ServerManager.Sites[siteName];
            return result;
        }



        public static List<SiteInfo> GetSitesInfo()
        {
            SitesInfo = new List<SiteInfo>();

            foreach (var site in ServerManager.Sites)
            {

                var siteInformation = new SiteInfo();


                siteInformation.Id = site.Id;
                siteInformation.Name = site.Name;
                //siteInformation.State = site.State;
                siteInformation.TraceFailedRequest = new TraceFailedRequest
                {
                    Directory = site.TraceFailedRequestsLogging.Directory,
                    Enabled = site.TraceFailedRequestsLogging.Enabled,
                    MaxLogFiles = site.TraceFailedRequestsLogging.MaxLogFiles
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
                    }); ;
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




                SitesInfo.Add(siteInformation);

            }

            return SitesInfo;
        }

        public const string RequestFilteringSectionName = "system.webServer/security/requestFiltering";

        public static ConfigurationElementCollection GetSiteDeniedUrls(string siteName)
        {
            Configuration config = ServerManager.GetWebConfiguration(siteName);
            ConfigurationSection requestFilteringSection = config.GetSection(RequestFilteringSectionName);
            ConfigurationElementCollection denyQueryStringSequencesCollection = requestFilteringSection.GetCollection("denyUrlSequences");
            return denyQueryStringSequencesCollection;
        }


        public string GetLogDirectory(Site site)
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
    }
}
