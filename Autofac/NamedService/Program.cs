using Autofac;
using Autofac.Features.AttributeFilters;
using NamedService.Database;

using static System.Console;

namespace NamedService
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<PostgresDatabase>()
                .Named<IDatabase>("postgres");

            builder.RegisterType<SqlServerDatabase>()
                .Named<IDatabase>("sql-server");

            builder.RegisterType<PostgresDatabase>()
                .Keyed<IDatabase>(Databases.Postgres);

            builder.RegisterType<SqlServerDatabase>()
                .Keyed<IDatabase>(Databases.SqlServer);

            builder.RegisterType<Executor>()
                .AsSelf()
                .WithAttributeFiltering();

            builder.RegisterType<DatabaseExecutor>()
                .AsSelf();


            using (IContainer container = builder.Build())
            using (ILifetimeScope scope = container.BeginLifetimeScope())
            {
                WriteLine("Resolving by name...");
                IDatabase database = scope.ResolveNamed<IDatabase>("postgres");
                database.Execute();

                database = scope.ResolveNamed<IDatabase>("sql-server");
                database.Execute();

                WriteLine();
                WriteLine();
                WriteLine("Resolving by keyed...");

                database = scope.ResolveKeyed<IDatabase>(Databases.SqlServer);
                database.Execute();

                database = scope.ResolveKeyed<IDatabase>(Databases.Postgres);
                database.Execute();

                WriteLine();
                WriteLine();
                WriteLine("Resolving with Attribute...");
                Executor executor = scope.Resolve<Executor>();
                executor.Execute();

                WriteLine();
                WriteLine();
                WriteLine("Resolving with Index...");
                DatabaseExecutor databaseExecutor = scope.Resolve<DatabaseExecutor>();
                databaseExecutor.OnPostgres();
                databaseExecutor.OnSqlServer();
            }

            ReadLine();
        }
    }
}
