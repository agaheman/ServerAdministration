using Microsoft.Web.Administration;

namespace ServerAdministration.IISServer
{
    public class LogFile
    {
        public string Directory { get; set; }
        public bool LocalTimeRollover { get; set; }
        public LogExtFileFlags LogExtFileFlags { get; set; }
        public bool Enabled { get; set; }
        public LoggingRolloverPeriod Period { get; set; }
        public LogFormat LogFormat { get; set; }
        public LogTargetW3C LogTargetW3C { get; set; }
        public long TruncateSize { get; set; }
    }
}
