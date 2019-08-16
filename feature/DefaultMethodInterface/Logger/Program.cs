using System;
using static System.Console;

namespace DefaultMethodInterface
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger logger = new Logger();

            logger.LogInformation("Some information");

            logger.LogError("Some Error");
            
            logger.Log("Any message", LogLevel.Error);

            logger.Log("Any message", LogLevel.Information);
        }
    }


}
