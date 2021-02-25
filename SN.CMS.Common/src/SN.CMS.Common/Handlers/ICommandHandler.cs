using SN.CMS.Common.Domain;
using SN.CMS.Common.Domain.Messages;
using System.Threading.Tasks;

namespace SN.CMS.Common.Handlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task HandleAsync(TCommand command, ICorrelationContext context);
    }
}
