namespace DefaultMethodInterface
{
    public enum LogLevel 
    {
        Error,
        Information
    }

    public interface ILogger
    {
        void LogError(string message);

        void LogInformation(string message);

        void Log(string message, LogLevel level)
        {
            switch(level)
            {
                case LogLevel.Error:
                    LogError(message);
                    break;
                case LogLevel.Information:
                    LogInformation(message);
                    break;
            }
        }
    }
}