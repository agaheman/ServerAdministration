using IISLogParser;
using System.Collections.Generic;
using System.Linq;

namespace ServerAdministration.IISServer
{
    public class LogParser
    {
        public IEnumerable<IISLogEvent> ParseIISLogs(string logFilePath)
        {
            List<IISLogEvent> logs = new List<IISLogEvent>();
            using (ParserEngine parser = new ParserEngine(logFilePath))
            {
                while (parser.MissingRecords)
                {
                    logs = parser.ParseLog().ToList();
                }
            }
            return logs;
        }

        public IEnumerable<IISLogEvent> ParseIISLogs(System.IO.FileInfo logFileInfo)
        {
            List<IISLogEvent> logs = new List<IISLogEvent>();
            using (ParserEngine parser = new ParserEngine(logFileInfo.FullName))
            {
                while (parser.MissingRecords)
                {
                    logs = parser.ParseLog().ToList();
                }
            }
            return logs;
        }

    }
}
