using SN.CMS.Identity.Domain;
using System;
using System.Threading.Tasks;

namespace SN.CMS.Identity.Repostories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);
        Task<User> GetAsync(string name);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
