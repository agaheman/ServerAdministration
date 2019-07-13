using System.ServiceProcess;
using System.Collections.Generic;

namespace ServerAdministration.WindowsOs.FolderWatcherService
{
    static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FolderWathcerService()//db address
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
