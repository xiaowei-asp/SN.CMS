using System;
using Newtonsoft.Json;

namespace SN.CMS.Identity.Messages.Events
{
    public class SignedIn
    {
        public Guid UserId { get; }

        [JsonConstructor]
        public SignedIn(Guid userId)
        {
            UserId = userId;
        }
    }
}
