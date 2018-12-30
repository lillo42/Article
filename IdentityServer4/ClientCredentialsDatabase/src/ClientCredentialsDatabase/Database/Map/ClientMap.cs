using FluentNHibernate.Mapping;

namespace ClientCredentialsDatabase.Database.Map
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Table($"{nameof(Client)}s");
            Id(x => x.Id).Length(200);

            HasManyToMany(x => x.GrantTypes)
                .LazyLoad()
                .Cascade.None()
                .Table("ClientGrantTypes");

            HasManyToMany(x => x.Scopes)
                .LazyLoad()
                .Cascade.All()
                .Table("ClientScopes");

            HasManyToMany(x => x.Secrets)
                .LazyLoad()
                .Cascade.All()
                .Table("ClientSecrets");
        }
    }
}
