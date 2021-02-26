using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SN.CMS.Identity.Domain;

namespace SN.CMS.Identity.Repostories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly SNCMSDbContext _context;

        public RefreshTokenRepository(SNCMSDbContext context)
        {
            _context = context;
        }
        public async Task<RefreshToken> GetAsync(string token)
        {
            var result = await _context.Set<RefreshToken>().FirstOrDefaultAsync(c => c.Token == token);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task AddAsync(RefreshToken token)
            => await _context.Set<RefreshToken>().AddAsync(token);

        public async Task UpdateAsync(RefreshToken token)
        {
            await Task.FromResult(_context.Set<RefreshToken>().Update(token));
            await _context.SaveChangesAsync();
        }
    }
}
