using System.Collections.Generic;

namespace PasswordDatabase.Database.Entity
{
    public class Secret
    {
        public virtual string Id { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<Client> Clients { get; set; } = new LinkedList<Client>();
    }
}
