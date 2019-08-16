using static System.Console;

namespace DefaultMethodInterface
{
    public class Logger : ILogger
    {
        public void LogError(string message)
        {
            Write("Error:");
            WriteLine(message);
        }

        public void LogInformation(string message)
        {
            Write("Information:");
            WriteLine(message);
        }
    }
}