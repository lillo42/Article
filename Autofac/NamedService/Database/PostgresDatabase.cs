using static System.Console;

namespace NamedService.Database
{
    public class PostgresDatabase : IDatabase
    {
        public void Execute()
        {
            WriteLine("Execute on Postgres....");
        }
    }
}
