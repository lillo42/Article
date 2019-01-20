using Autofac.Features.Indexed;
using NamedService.Database;

namespace NamedService
{
    public class DatabaseExecutor
    {
        private readonly IIndex<Databases, IDatabase> _database;

        public DatabaseExecutor(IIndex<Databases, IDatabase> database)
        {
            _database = database;
        }

        public void OnSqlServer() => _database[Databases.SqlServer].Execute();

        public void OnPostgres() => _database[Databases.Postgres].Execute();
    }
}
