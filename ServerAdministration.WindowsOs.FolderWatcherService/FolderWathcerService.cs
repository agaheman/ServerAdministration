using System.IO;
using System.ServiceProcess;

namespace ServerAdministration.WindowsOs.FolderWatcherService
{
    public partial class FolderWathcerService : ServiceBase
    {
        private FileSystemWatcher myWatcher;
        private long SizeThreshold;

        public FolderWathcerService(string path)
        {
            myWatcher = new FileSystemWatcher(path);

            myWatcher.Changed += MyWatcher_Changed1; ;
            myWatcher.EnableRaisingEvents = true;


            InitializeComponent();
        }

        private void MyWatcher_Changed1(object sender, FileSystemEventArgs e)
        {
            var wathcer = sender as FileSystemWatcher;

            if (wathcer.NotifyFilter == NotifyFilters.Size)
            {
                driveInfo = GetDriveInfo(wathcer.Path);

                if (driveInfo.AvailableFreeSpaceByte < SizeThreshold)
                    throw new NotEnoughDiskSpaceException($"Your database drive free space is {driveInfo.AvailableFreeSpace}");
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
