using System;
using Microsoft.AspNetCore.Identity;
using SN.CMS.Common;
using SN.CMS.Common.Domain.AggregateRoots;

namespace SN.CMS.Identity.Domain
{
    public class User:AggregateRoot
    {
        public string Name { get; private set; }
        public string Role { get; private set; }
        public string PasswordHash { get; private set; }

        public User()
        {

        }

        public User(Guid id, string name, string role)
        {
            if (!Domain.Role.IsValid(role))
            {
                throw new Exception($"Invalid role: '{role}'.");
            }
            Id = id;
            Name = name;
            Role = role;
        }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            if (password.IsNullOrEmpty())
            {
                throw new Exception("密码不能为空.");
            }

            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
        {
            var pwd = passwordHasher.VerifyHashedPassword(this, PasswordHash, password);
            return pwd != PasswordVerificationResult.Failed;
        }
    }
}
