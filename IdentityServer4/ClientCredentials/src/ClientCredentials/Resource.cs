using IdentityServer4.Models;
using System.Collections.Generic;

namespace ClientCredentials
{
    public class Resource
    {
        internal static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("apiTest", "API V1")
            };
        }

        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "test",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("123".Sha256()) },
                    AllowedScopes = { "apiTest" }
                }
            };
        }   
    }
}
