using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SN.CMS.Identity.Domain;

namespace SN.CMS.Identity.Repostories
{
    public class UserRepository:IUserRepository
    {
        private readonly SNCMSDbContext _context;

        public UserRepository(SNCMSDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetAsync(Guid id)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<User> GetAsync(string name)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(c => c.Name == name);
        }

        public async Task AddAsync(User user)
        {
            await _context.Set<User>().AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            await Task.FromResult(_context.Set<User>().Update(user));
            await _context.SaveChangesAsync();
        }
    }
}
