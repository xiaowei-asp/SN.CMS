using System;
using Newtonsoft.Json;

namespace SN.CMS.Identity.Messages.Events
{
    public class SignedUp
    {
        public Guid UserId { get; }

        public string Name { get; }

        public string Role { get; }

        [JsonConstructor]
        public SignedUp(Guid userId, string name, string role)
        {
            UserId = userId;
            Name = name;
            Role = role;
        }
    }
}
