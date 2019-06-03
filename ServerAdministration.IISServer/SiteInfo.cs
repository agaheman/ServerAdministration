using Microsoft.Web.Administration;
using System.Collections.Generic;

namespace ServerAdministration.IISServer
{
    public class SiteInfo
    {
        public int Id { get; set; }
        public long SiteId { get; set; }
        public string SiteName { get; set; }
        public bool ServerAutoStart { get; set; }

        public ObjectState State { get; set; }
        public TraceFailedRequest TraceFailedRequest { get; set; }
        public List<Binding> Bindings { get; set; }
        public LogFile LogFile { get; set; }

        public List<App> Applications { get; set; }
    }
}
