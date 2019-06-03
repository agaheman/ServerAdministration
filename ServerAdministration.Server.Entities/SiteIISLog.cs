using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAdministration.Server.Entities
{
    public class SiteIISLog : IEntity
    {
        public int Id { get; set; }
        /// <summary>
        /// Slave unique Id in Master DB
        /// </summary>
        public int SlaveServerId { get; set; }

        public string SiteAppPath { get; set; }
        public IISLogEvent IISLogEvent { get; set; }
    }
}
