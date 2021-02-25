using System;
using System.Threading.Tasks;
using SN.CMS.Common.Authentications;
using SN.CMS.Identity.Domain;

namespace SN.CMS.Identity.Services
{
    public interface IIdentityServices
    {
        Task SignUpAsync(Guid id, string name, string password, string role = Role.User);

        Task<JsonWebToken> SignInAsync(string name, string password);
    }
}
