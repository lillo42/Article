using FluentNHibernate.Mapping;
using Simple.Model;

namespace Simple.Mapper
{
    public class AuthorMapper : ClassMap<Author>
    {
        public AuthorMapper()
        {
            Id(x => x.Id)
                .GeneratedBy.Identity();

            Map(x => x.Name)
                .Length(100)
                .Not.Nullable();

            HasMany(x => x.Books);
        }
    }
}