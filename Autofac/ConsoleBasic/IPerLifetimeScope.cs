using System;

namespace ConsoleBasic
{
    public interface IPerLifetimeScope
    {
        IPerDependency Dependency { get; }
        Guid Id { get;  }
    }
}