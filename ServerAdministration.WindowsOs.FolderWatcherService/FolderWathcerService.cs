using System;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Collections.Generic;

namespace ServerAdministration.WindowsOs.FolderWatcherService
{
    public partial class FolderWathcerService : ServiceBase
    {
        private FileSystemWatcher myWatcher;
        private long SizeThreshold = 470000000;

        public FolderWathcerService(List<string> paths)
        {
            foreach (var path in paths)
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

        private void MyWatcher_Created(object sender, FileSystemEventArgs e)
        {
            var wathcer = sender as FileSystemWatcher;
            DirectoryInfo logDirectory = new DirectoryInfo(wathcer.Path);

            var fileToParse = logDirectory.GetFiles("*.log")
                .OrderByDescending(f => f.LastWriteTime)
                .ElementAt(2);

            IISServer.ManagementUnit.SaveParsedLog(fileToParse);
        }


        private void MyWatcher_Changed(object sender, FileSystemEventArgs e)
        {
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
