using System.ServiceProcess;

namespace ServerAdministration.WindowsOs.FolderWatcherService
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FolderWathcerService(@"F:\Amoozesh")//db address
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
