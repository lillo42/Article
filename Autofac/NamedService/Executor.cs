using Autofac.Features.AttributeFilters;
using NamedService.Database;

namespace NamedService
{
    public class Executor
    {
        private readonly IDatabase _database;

        public Executor([KeyFilter(Databases.Postgres)]IDatabase database)
        {
            _database = database;
        }

        public void Execute() => _database.Execute();
    }
}
