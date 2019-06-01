using Microsoft.Web.Administration;

namespace ServerAdministration.IISServer
{
    public class App
    {
        public string ApplicationPoolName { get; set; }
        public string EnabledProtocols { get; set; }
        public string Path { get; set; }
        public bool EnabledPreload { get; set; }
        public ConfigurationElementCollection DeniedUrls { get; set; }

    }
}
