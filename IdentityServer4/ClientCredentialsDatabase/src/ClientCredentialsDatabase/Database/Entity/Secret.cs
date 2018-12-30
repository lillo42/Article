using System.Collections.Generic;

namespace ClientCredentialsDatabase.Database
{
    public class Secret
    {
        public virtual string Id { get; set; }
        public virtual ICollection<Client> Clients { get; set; } = new LinkedList<Client>();
    }
}
