using static System.Console;

namespace NamedService.Database
{
    public class SqlServerDatabase : IDatabase
    {
        public void Execute()
        {
            WriteLine("Execute on SQL Server....");
        }
    }
}
