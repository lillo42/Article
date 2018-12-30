using FluentNHibernate.Mapping;

namespace ClientCredentialsDatabase.Database.Map
{
    public class ScopeMap : ClassMap<Scope>
    {
        public ScopeMap()
        {
            Table($"{nameof(Scope)}s");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Length(50).Not.Nullable();
            Map(x => x.DisplayName).Length(100);
            Map(x => x.Description);
            Map(x => x.Required);
            Map(x => x.Emphasize);
            Map(x => x.ShowInDiscoveryDocument);

            HasManyToMany(x => x.Resources)
                .LazyLoad()
                .Cascade.All()
                .Table("ResourceScopes");

            HasManyToMany(x => x.Clients)
                .LazyLoad()
                .Cascade.All()
                .Inverse()
                .Table("ClientScopes");
        }
    }
}
