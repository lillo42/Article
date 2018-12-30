using IdentityServer4.Models;
using IdentityServer4.Validation;
using NHibernate;
using NHibernate.Linq;
using PasswordDatabase.Database.Entity;
using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PasswordDatabase.IdentityServer
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly ISession _session;

        public ResourceOwnerPasswordValidator(ISession session)
        {
            _session = session ?? throw new ArgumentNullException(nameof(session));
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var md5 = MD5.Create();
            string hash = CreateMd5(context.Password);

            User user = await _session.Query<User>().FirstOrDefaultAsync(x => x.UserName == context.UserName && x.Password == hash);
            if (user == null)
                context.Result = new GrantValidationResult(TokenRequestErrors.InvalidRequest, "invalid username or password");
            else
            {
                context.Result = new GrantValidationResult(
                    subject: user.Id,
                    authenticationMethod: "custom");
            }
        }

        private string CreateMd5(string input)
        {
            using (MD5 md = MD5.Create())
            {
                byte[] data = Encoding.UTF8.GetBytes(input);
                byte[] hash = md.ComputeHash(data);

                var sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
