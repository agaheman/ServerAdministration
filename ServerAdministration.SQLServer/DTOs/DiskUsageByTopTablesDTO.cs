using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAdministration.SQLServer.DTOs
{
    public class DiskUsageByTopTablesDTO
    {
        public string Schema { get; set; }
        public string Table { get; set; }
        public string RowCount { get; set; }

        public float DataSizeMB { get; set; }
        public float DataSizeGB => DataSizeMB / 1024;

        public float IndexSizeMB { get; set; }
        public float IndexSizeGB => IndexSizeMB / 1024;
    }
}
