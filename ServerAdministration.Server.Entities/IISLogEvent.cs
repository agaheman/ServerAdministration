using System;

namespace ServerAdministration.Server.Entities
{
    public class IISLogEvent:IEntity
    {
        public string Id { get; set; }
        public string ClientIp { get; set; }
        public int? ServerPort { get; set; }
        public string ServerIp { get; set; }
        /// <summary>
        /// Server Name
        /// </summary>
        public string Computername { get; set; }
        /// <summary>
        /// Service Name
        /// </summary>
        public string Sitename { get; set; }
        public DateTime DateTimeEvent { get; set; }
        public int? SentBytes { get; set; }
        public int? RecievedBytes { get; set; }
        public int? TimeTaken { get; set; }
        public long? Win32Status { get; set; }
        public int? ProtocolStatus { get; set; }
        public int? ProtocolSubstatus { get; set; }
        public string Host { get; set; }
        public string Referer { get; set; }
        public string Cookie { get; set; }
        public string UserAgent { get; set; }
        public string ProtocolVersion { get; set; }
        public string Username { get; set; }
        public string UriQuery { get; set; }
        public string UriStem { get; set; }
        public string Method { get; set; }
    }
}
