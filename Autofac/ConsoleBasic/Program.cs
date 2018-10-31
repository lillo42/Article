using Autofac;
using static System.Console;

namespace ConsoleBasic
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Singleton>()
                .AsSelf()
                .SingleInstance();

            builder.RegisterType<PerDependency>()
                .As<IPerDependency>()
                .InstancePerDependency();

            builder.RegisterType<PerLifetimeScope>()
                .As<IPerLifetimeScope>()
                .InstancePerLifetimeScope();

            IContainer container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                WriteLine("Begin LifetimeScope 1");

                Write($"Get {nameof(Singleton)}: ");
                var singleton = scope.Resolve<Singleton>();
                WriteLine($"Id: {singleton.Id}");


                Write($"Get {nameof(IPerDependency)} - 1: ");
                var dependency1 = scope.Resolve<IPerDependency>();
                WriteLine($"Id: {dependency1.Id}");

                Write($"Get {nameof(IPerDependency)} - 2: ");
                var dependency2 = scope.Resolve<IPerDependency>();
                WriteLine($"Id: {dependency2.Id}");

                Write($"Get {nameof(IPerLifetimeScope)} - 1: ");
                var lifetimeScope1 = scope.Resolve<IPerLifetimeScope>();
                Write($"Id: {lifetimeScope1.Id} - ");
                WriteLine($"Dependency Id: {lifetimeScope1.Dependency.Id} - ");


                Write($"Get {nameof(IPerLifetimeScope)} - 2: ");
                var lifetimeScope2 = scope.Resolve<IPerLifetimeScope>();
                Write($"Id: {lifetimeScope2.Id} - ");
                WriteLine($"Dependency Id: {lifetimeScope2.Dependency.Id} - ");

                WriteLine("End LifetimeScope 1");
            }

            WriteLine();

            using (var scope = container.BeginLifetimeScope())
            {
                WriteLine("Begin LifetimeScope 2");

                Write($"Get {nameof(Singleton)}: ");
                var singleton = scope.Resolve<Singleton>();
                WriteLine($"Id: {singleton.Id}");


                Write($"Get {nameof(IPerDependency)} - 1: ");
                var dependency1 = scope.Resolve<IPerDependency>();
                WriteLine($"Id: {dependency1.Id}");

                Write($"Get {nameof(IPerDependency)} - 2: ");
                var dependency2 = scope.Resolve<IPerDependency>();
                WriteLine($"Id: {dependency2.Id}");

                Write($"Get {nameof(IPerLifetimeScope)} - 1: ");
                var lifetimeScope1 = scope.Resolve<IPerLifetimeScope>();
                Write($"Id: {lifetimeScope1.Id} - ");
                WriteLine($"Dependency Id: {lifetimeScope1.Dependency.Id} - ");


                Write($"Get {nameof(IPerLifetimeScope)} - 2: ");
                var lifetimeScope2 = scope.Resolve<IPerLifetimeScope>();
                Write($"Id: {lifetimeScope2.Id} - ");
                WriteLine($"Dependency Id: {lifetimeScope2.Dependency.Id} - ");

                WriteLine("End LifetimeScope 2");
            }

            ReadLine();
        }
    }
}