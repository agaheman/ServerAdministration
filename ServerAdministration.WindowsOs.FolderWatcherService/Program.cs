using System.ServiceProcess;
using System.Collections.Generic;

namespace ServerAdministration.WindowsOs.FolderWatcherService
{
    static class Program
    {
        static void Main(List<string> paths)
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new FolderWathcerService(paths)//db address
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}
