using System.Collections.Generic;

namespace ClientCredentialsDatabase.Database
{
    public class Scope
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual string Description { get; set; }
        public virtual bool Required { get; set; }
        public virtual bool Emphasize { get; set; }
        public virtual bool ShowInDiscoveryDocument { get; set; }
        public virtual ICollection<Resource> Resources { get; set; } = new LinkedList<Resource>();
        public virtual ICollection<Client> Clients { get; set; } = new LinkedList<Client>();
    }
}
