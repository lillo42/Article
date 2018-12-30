using FluentNHibernate.Mapping;
using PasswordDatabase.Database.Entity;

namespace PasswordDatabase.Database.Map
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table($"{nameof(User)}s");
            Id(x => x.Id).Length(200);

            Map(x => x.IsActive).Not.Nullable();
            Map(x => x.UserName).Length(200).Not.Nullable();
            Map(x => x.Password).Not.Nullable();
        }
    }
}
