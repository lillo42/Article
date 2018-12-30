using IdentityServer4.Models;
using IdentityServer4.Services;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PasswordDatabase.IdentityServer
{
    public class ProfileService : IProfileService
    {
        private readonly ISession _session;

        public ProfileService(ISession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            Claim claim = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");
            if (claim?.Value != null)
                context.IssuedClaims = context.Subject.Claims.ToList();
            return Task.CompletedTask;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            Claim claim = context.Subject.Claims.FirstOrDefault(x => x.Type == "sub");
            if (claim?.Value != null)
            {
                string userId = claim.Value;
                context.IsActive = await _session.Query<Database.Entity.User>().Where(x => x.Id == userId).Select(x => x.IsActive).FirstOrDefaultAsync();
            }
            else
                context.IsActive = false;
        }
    }
}
