using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SN.CMS.Common.Domain;
using SN.CMS.Identity.Domain;
using SN.CMS.Identity.Messages.Commands;

namespace SN.CMS.Identity
{
    public class SNCMSDbContext:DbContext
    {
        private readonly IOptions<DbOptions> _dbOptions;

        public SNCMSDbContext(IOptions<DbOptions> sqlOptions) : base()
        {
            _dbOptions = sqlOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            optionsBuilder.UseSqlite(_dbOptions.Value.ConnectionString);
        }
        public DbSet<User> User { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        
    }
}
