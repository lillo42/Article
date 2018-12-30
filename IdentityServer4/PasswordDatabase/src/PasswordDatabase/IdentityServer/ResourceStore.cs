using IdentityServer4.Models;
using IdentityServer4.Stores;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PasswordDatabase.IdentityServer
{
    public class ResourceStore : IResourceStore
    {
        private readonly ISession _session;

        public ResourceStore(ISession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task<ApiResource> FindApiResourceAsync(string name)
        {
            var resource = await _session.Query<Database.Entity.Resource>().FirstOrDefaultAsync(x => x.Name == name);
            if (resource != default)
            {
                return ToApiResource(resource);
            }
            return null;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            ICollection<ApiResource> resources = new LinkedList<ApiResource>();
            foreach (string scope in scopeNames)
            {
                var scopes = await _session.Query<Database.Entity.Scope>().FirstOrDefaultAsync(x => x.Name == scope);
                if (scopes != default)
                {
                    foreach (var resource in scopes.Resources)
                        resources.Add(ToApiResource(resource));
                }
            }
            return resources;
        }

        public Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeAsync(IEnumerable<string> scopeNames)
        {
            return Task.FromResult(Enumerable.Empty<IdentityResource>());
        }

        public Task<Resources> GetAllResourcesAsync()
        {
            return Task.FromResult(new Resources
            {
                OfflineAccess = false,
                ApiResources = _session.Query<Database.Entity.Resource>().AsEnumerable().Select(x => ToApiResource(x)).ToList(),
                IdentityResources = Enumerable.Empty<IdentityResource>().ToList()
            });
        }

        private ApiResource ToApiResource(Database.Entity.Resource resource)
        {
            return new ApiResource
            {
                Name = resource.Name,
                DisplayName = resource.DisplayName,
                Enabled = resource.Enabled,
                Description = resource.Description,
                Scopes = resource.Scopes.Select(x => new Scope
                {
                    Name = x.Name,
                    Description = x.Description,
                    DisplayName = x.DisplayName,
                    Emphasize = x.Emphasize,
                    Required = x.Required,
                    ShowInDiscoveryDocument = x.ShowInDiscoveryDocument
                }).ToList()
            };
        }
    }
}
