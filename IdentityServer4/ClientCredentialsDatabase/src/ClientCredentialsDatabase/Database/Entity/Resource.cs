using System.Collections.Generic;

namespace ClientCredentialsDatabase.Database
{
    public class Resource
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual string DisplayName { get; set; }
        public virtual bool Enabled { get; set; }
        public virtual string Description { get; set; }
        public virtual ICollection<Scope> Scopes { get; set; } = new LinkedList<Scope>();
    }
}
