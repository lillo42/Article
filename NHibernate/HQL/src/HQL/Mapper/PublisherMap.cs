using FluentNHibernate.Mapping;
using HQL.Model;

namespace HQL.Mapper
{
    public class PublisherMap : ClassMap<Publisher>
    {
        public PublisherMap()
        {
            Cache.ReadOnly();

            Table("TB_PUBLISHERS");

            Id(x => x.Id)
                .Column("PBL_ID")
                .GeneratedBy.Identity();

            Map(x => x.Name)
                .Column("PBL_NAME")
                .Length(50);


            HasMany(x=>x.Books)
                .Cascade.None();
        }
    }
}
