using SN.CMS.Common.Domain;
using System.Threading.Tasks;

namespace SN.CMS.Common.Dispatchers.Commands
{
    public interface ICommandDispatcher
    {
        Task SendAsync<T>(T command) where T : ICommand;
    }
}
