using System.Collections.Generic;

namespace ClientCredentialsDatabase.Database
{
    public class Client
    {
        public virtual string Id { get; set; }
        public virtual ICollection<Scope> Scopes { get; set; } = new LinkedList<Scope>();
        public virtual ICollection<Secret> Secrets { get; set; } = new LinkedList<Secret>();
        public virtual ICollection<GrantType> GrantTypes { get; set; } = new LinkedList<GrantType>();
    }
}
