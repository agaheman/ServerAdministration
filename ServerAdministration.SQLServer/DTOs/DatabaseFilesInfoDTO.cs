using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAdministration.SQLServer.DTOs
{
    public class DatabaseFilesInfoDTO
    {
        public string DatabaseName { get; set; }
        public string LogicalName { get; set; }
        public string PhysicalName { get; set; }
        public string Path { get; set; }
        public float Size { get; set; }

    }
}
