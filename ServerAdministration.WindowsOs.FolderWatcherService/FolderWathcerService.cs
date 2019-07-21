using Newtonsoft.Json;
using ServerAdministration.IISServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.ServiceProcess;
using System.Threading;

namespace ServerAdministration.WindowsOs.FolderWatcherService
{
    public partial class FolderWathcerService : ServiceBase
    {
        private FileSystemWatcher myWatcher;
        private long SizeThreshold = 470000000;
        private List<SiteInfo> sitesInfo;
        public static ILogger logger;

        public FolderWathcerService()
        {
            sitesInfo = GetListOfSites();
            logger = new NLogAddapter(NLoggerClass.FolderWatcherServive);



            foreach (var path in sitesInfo.Select(x => x.LogFile.Directory))
            {
                var fileSystemWatcher = new FileSystemWatcher
                {
                    Path = path,
                    EnableRaisingEvents = true,
                };

                fileSystemWatcher.Created += MyWatcher_Created;
            }


            InitializeComponent();
        }

        public List<SiteInfo> GetListOfSites()
        {
            logger.LogInfo("GetListOfSites()");
            var apiUrl = $"https://localhost:5005/api/SiteInfo/GetSitesInfo";

            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, apiUrl))
            {
                using (var response = client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, CancellationToken.None).Result)
                {
                    logger.LogInfo(
                        response.StatusCode.ToString() +
                        Environment.NewLine +
                        response.Content != null ? response.Content.ToString() : "Content is not null.");

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = response.Content.ReadAsStringAsync();
                        logger.LogInfo(JsonConvert.DeserializeObject<List<SiteInfo>>(responseContent.Result));

                        return JsonConvert.DeserializeObject<List<SiteInfo>>(responseContent.Result);
                    }

                    throw new Exception("Response is not Successfull");
                }

            }
        }
        private void MyWatcher_Created(object sender, FileSystemEventArgs e)
        {
            var wathcer = sender as FileSystemWatcher;
            DirectoryInfo logDirectory = new DirectoryInfo(wathcer.Path);


            logger.LogInfo(
                Environment.NewLine +
                logDirectory +
                Environment.NewLine);

            var fileToParse = logDirectory.GetFiles("*.log")
                .OrderByDescending(f => f.LastWriteTime)
                .ElementAt(2);

            logger.LogInfo(
                Environment.NewLine +
                fileToParse.FullName +
                  Environment.NewLine);

            ManagementUnit.SaveParsedLog(fileToParse);
        }


        private void MyWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            logger.LogInfo(
              "MyWatcher_Changed()");

            var wathcer = sender as FileSystemWatcher;

            if (wathcer.NotifyFilter == NotifyFilters.Size)
            {

                var driveInfo = DriveInfo.GetDrives()
                  .First(d => d.IsReady && d.RootDirectory.ToString() == Path.GetPathRoot(wathcer.Path));

                File.WriteAllText(@"F:\log\Service3.txt", $"New Size=: {driveInfo.AvailableFreeSpace }");

                if (driveInfo.AvailableFreeSpace < SizeThreshold)
                {

                    throw new Exception($"Not Enough Disk Space Exception {Environment.NewLine}" +
                        $"Your {driveInfo.Name} drive free space is {driveInfo.AvailableFreeSpace}");
                }
            }
        }

        protected override void OnStart(string[] args)
        {
        }

        protected override void OnStop()
        {
        }

    }
}
