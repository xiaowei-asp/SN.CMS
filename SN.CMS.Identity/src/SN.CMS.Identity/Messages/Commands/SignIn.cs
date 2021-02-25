using Newtonsoft.Json;
using SN.CMS.Common.Domain;

namespace SN.CMS.Identity.Messages.Commands
{
    public class SignIn:ICommand
    {
        public string Name { get; }
        public string Password { get; }

        [JsonConstructor]
        public SignIn(string name, string password)
        {
            Name = name;
            Password = password;
        }
    }
}
