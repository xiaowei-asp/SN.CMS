using System.Threading.Tasks;
using SN.CMS.Identity.Domain;

namespace SN.CMS.Identity.Repostories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);

        Task AddAsync(RefreshToken token);
    }
}
