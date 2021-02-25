using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SN.CMS.Identity.Messages.Commands;

namespace SN.CMS.Identity.Repostories
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken> GetAsync(string token);

        Task AddAsync(RefreshToken token);
    }
}
