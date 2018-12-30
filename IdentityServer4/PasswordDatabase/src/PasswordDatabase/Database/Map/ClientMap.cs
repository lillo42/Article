﻿using FluentNHibernate.Mapping;
using PasswordDatabase.Database.Entity;

namespace PasswordDatabase.Database.Map
{
    public class ClientMap : ClassMap<Client>
    {
        public ClientMap()
        {
            Table($"{nameof(Client)}s");
            Id(x => x.Id).Length(200);
            Map(x => x.Name).Length(200);

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
