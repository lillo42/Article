using FluentNHibernate.Mapping;
using HQL.Model;

namespace HQL.Map
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