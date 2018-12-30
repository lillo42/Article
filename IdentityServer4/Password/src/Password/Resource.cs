using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;

namespace Password
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
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets = { new Secret("123".Sha256()) },
                    AllowedScopes = { "apiTest" }
                }
            };
        }

        internal static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "test",
                    Password = "123456"
                }
            };
        }
    }
}
