using FluentNHibernate.Mapping;
using System.Collections.Generic;

namespace ClientCredentialsDatabase.Database.Map
{
    public class ResourceMap : ClassMap<Resource>
    {
        public ResourceMap()
        {
            Table($"{nameof(Resource)}s");
            Id(x => x.Id).GeneratedBy.Identity();
            Map(x => x.Name).Length(50).Not.Nullable();
            Map(x => x.DisplayName).Length(200);
            Map(x => x.Description);
            Map(x => x.Enabled);

            HasManyToMany(x => x.Scopes)
                .LazyLoad()
                .Cascade.All()
                .Inverse()
                .Table("ResourceScopes");
        }
    }
}
