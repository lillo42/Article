using FluentNHibernate.Mapping;
using HQL.Model;

namespace HQL.Map
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