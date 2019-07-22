namespace ServerAdministration.Test
{
    public interface ILogger
    {
        void LogInfo(string message);
        void LogInfo(object theObject);

        void LogInfoAsync(string message);
        void LogInfoAsync(object theObject);
    }
}
