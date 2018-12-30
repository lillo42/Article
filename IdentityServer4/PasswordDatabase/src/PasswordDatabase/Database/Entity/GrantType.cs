using System.Collections.Generic;

namespace PasswordDatabase.Database.Entity
{
    public class GrantType
    {
        public virtual int Id { get; set; }
        public virtual string Type { get; set; }
        public virtual ICollection<Client> Clients { get; set; }
    }
}
