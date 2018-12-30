using IdentityServer4.Models;
using IdentityServer4.Stores;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordDatabase.IdentityServer
{
    public class ClientStore : IClientStore
    {
        private readonly ISession _session;

        public ClientStore(ISession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task<Client> FindClientByIdAsync(string clientId)
        {
            var client = await _session.Query<Database.Entity.Client>().FirstOrDefaultAsync(x => x.Id == clientId);
            if (client != default)
            {
                return new Client
                {
                    ClientId = client.Id,
                    AllowedGrantTypes = client.GrantTypes.Select(x => x.Type).ToList(),
                    ClientSecrets = client.Secrets.Select(x => new Secret(x.Id.Sha256())).ToList(),
                    AllowedScopes = client.Scopes.Select(x => x.Name).ToList()
                };
            }
            return null;
        }
    }
}
