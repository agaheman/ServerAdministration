using NLog;
using System;
using System.Threading.Tasks;

namespace ServerAdministration.WindowsOs.FolderWatcherService
{
    public enum NLoggerClass
    {
        FolderWatcherServive,
    }
    public class NLogAddapter : ILogger
    {

        private Logger nLogger;
        public NLogAddapter(Type type)
        {
            nLogger = LogManager.GetCurrentClassLogger();
        }
        public NLogAddapter(NLoggerClass loggerClass)
        {
            nLogger = LogManager.GetLogger(loggerClass.ToString());
        }
        public void LogInfoAsync(string message)
        {
            Task.Run(() => LogInfo(message));
        }

        public void LogInfoAsync(object theObject)
        {
            Task.Run(() => LogInfo(theObject));
        }

        public void LogInfo(string message)
        {
            nLogger.Info(message);
        }

        public void LogInfo(object theObject)
        {
            nLogger.Info(theObject);
        }
    }
}
