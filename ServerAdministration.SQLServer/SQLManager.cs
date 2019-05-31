using System;
using System.Collections.Generic;
using System.Text;

namespace ServerAdministration.SQLServer
{
    public class SQLManager
    {
        public SQLManager()
        {

        }
        public bool IsSqlServerAgentRunning()
        {
            return false;
            //return GetContext().Database.SqlQuery<int>(sqlServerAgentRunningQuery).FirstOrDefault() == 0 ? false : true;
        }
    }
}
