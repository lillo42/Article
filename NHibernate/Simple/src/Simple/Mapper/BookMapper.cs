using FluentNHibernate.Mapping;
using Simple.Model;

namespace Simple.Mapper
{
    public class BookMapper : ClassMap<Book>
    {
        public BookMapper()
        {
            Id(x => x.Id)
                .GeneratedBy.Identity();
            
            Map(x => x.Name)
                .Length(100)
                .Not.Nullable();

            Map(x => x.Description)
                .Length(250)
                .Nullable();

            References(x => x.Author)
                .Not.Nullable();

            HasManyToMany(x => x.Collaborators)
                .Cascade.None();
        }
    }
}