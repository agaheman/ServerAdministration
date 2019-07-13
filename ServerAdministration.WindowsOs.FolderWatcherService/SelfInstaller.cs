using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServerAdministration.WindowsOs.FolderWatcherService
{
    public static class SelfInstaller
    {
        public static bool InstallMe(string ServiceExePath)
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { ServiceExePath });
            }
            catch
            {
                return false;
            }
            return true;
        }

        public static bool UninstallMe(string ServiceExePath)
        {
            try
            {
                ManagedInstallerClass.InstallHelper(new string[] { "/u", ServiceExePath });
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
