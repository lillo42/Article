using FluentNHibernate.Mapping;
using PasswordDatabase.Database.Entity;

namespace PasswordDatabase.Database.Map
{
    public class GrantTypeMap : ClassMap<GrantType>
    {
        public GrantTypeMap()
        {
            Table($"{nameof(GrantType)}s");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Type).Length(100).Not.Nullable();

            HasManyToMany(x => x.Clients)
                .LazyLoad()
                .Cascade.All()
                .Inverse()  
                .Table("ClientGrantTypes");
        }
    }
}
