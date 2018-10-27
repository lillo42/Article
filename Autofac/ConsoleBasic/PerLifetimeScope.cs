using System;

namespace ConsoleBasic
{
    public class PerLifetimeScope : IPerLifetimeScope
    {
        public IPerDependency Dependency { get; }
        public Guid Id { get;  } = Guid.NewGuid();

        public PerLifetimeScope(IPerDependency dependency)
        {
            Dependency = dependency;
        }
    }
}