using Microsoft.Web.Administration;
using System.Collections.Generic;

namespace ServerAdministration.IISServer
{
    public class SiteInfo
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool ServerAutoStart { get; set; }

        public ObjectState State { get; set; }
        public TraceFailedRequest TraceFailedRequest { get; set; }
        public List<Binding> Bindings { get; set; }
        public LogFile LogFile { get; set; }

        public List<App> Applications { get; set; }
    }
}
