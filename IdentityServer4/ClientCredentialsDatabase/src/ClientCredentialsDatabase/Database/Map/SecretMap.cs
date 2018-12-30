using FluentNHibernate.Mapping;

namespace ClientCredentialsDatabase.Database.Map
{
    public class SecretMap : ClassMap<Secret>
    {
        public SecretMap()
        {
            Table($"{nameof(Secret)}s");
            Id(x => x.Id).Length(200);

            HasManyToMany(x => x.Clients)
                .LazyLoad()
                .Cascade.All()
                .Inverse()
                .Table("ClientSecrets");
        }
    }
}
