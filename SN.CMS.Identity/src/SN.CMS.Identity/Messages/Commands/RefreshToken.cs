using System;
using Microsoft.AspNetCore.Identity;
using SN.CMS.Common.Domain.AggregateRoots;
using SN.CMS.Identity.Domain;

namespace SN.CMS.Identity.Messages.Commands
{
    public class RefreshToken : AggregateRoot
    {
        public Guid UserId { get; private set; }
        public string Token { get; private set; }

        public RefreshToken(User user, IPasswordHasher<User> passwordHasher)
        {
            UserId = user.Id;
            Token = CreateToken(user, passwordHasher);
        }

        protected RefreshToken()
        {
        }

        private static string CreateToken(User user, IPasswordHasher<User> passwordHasher)
            => passwordHasher.HashPassword(user, Guid.NewGuid().ToString("N"))
                .Replace("=", string.Empty)
                .Replace("+", string.Empty)
                .Replace("/", string.Empty);
    }
}
