using System;
using Newtonsoft.Json;
using SN.CMS.Common.Domain;

namespace SN.CMS.Identity.Messages.Commands
{
    public class SignUp:ICommand
    {
        public Guid Id { get; }
        public string Name { get; }
        public string Password { get; }
        public string Role { get; }

        [JsonConstructor]
        public SignUp(Guid id, string name, string password, string role)
        {
            Id = id;
            Name = name;
            Password = password;
            Role = role;
        }
    }
}
