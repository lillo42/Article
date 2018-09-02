using FluentNHibernate.Mapping;
using Simple.Model;

namespace Simple.Mapper
{
    public class CollaboratorMapper : ClassMap<Collaborator>
    {
        public CollaboratorMapper()
        {
            Id(x => x.Id)
                .GeneratedBy.Identity();
            
            Map(x => x.Name)
                .Length(100)
                .Not.Nullable();
        }
    }
}