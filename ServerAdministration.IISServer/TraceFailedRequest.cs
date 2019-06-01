namespace ServerAdministration.IISServer
{
    public class TraceFailedRequest
    {
        public string Directory { get; set; }
        public bool Enabled { get; set; }
        public long MaxLogFiles { get; set; }
    }
}
