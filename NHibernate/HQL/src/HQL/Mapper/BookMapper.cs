using FluentNHibernate.Mapping;
using HQL.Model;

namespace HQL.Map
{
    public class BookMapper : ClassMap<Book>
    {
        public BookMapper()
        {
            Table("TB_BOOKS");

            Id(x => x.Id)
                .Column("BKS_ID")
                .GeneratedBy.Identity();
            
            Map(x => x.Name)
                .Column("BKS_NAME")
                .Length(100)
                .Not.Nullable();

            Map(x => x.Description)
                .Column("BKS_DESCRIPTION")
                .Length(250)
                .Nullable();

            References(x => x.Author)
                .Not.Nullable();

            References(x => x.Publisher)
                .Not.Nullable();

            HasManyToMany(x => x.Collaborators)
                .Cascade.All();
        }
    }
}