using System;

namespace ConsoleBasic
{
    public class PerDependency : IPerDependency
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}