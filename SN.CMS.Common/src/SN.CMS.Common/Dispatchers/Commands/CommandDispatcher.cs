using Autofac;
using SN.CMS.Common.Domain;
using SN.CMS.Common.Domain.Messages;
using SN.CMS.Common.Handlers;
using System.Threading.Tasks;

namespace SN.CMS.Common.Dispatchers.Commands
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IComponentContext _context;

        public CommandDispatcher(IComponentContext context)
        {
            _context = context;
        }
        public async Task SendAsync<T>(T command) where T : ICommand
        {
            var handler = _context.Resolve<ICommandHandler<T>>();
            await handler.HandleAsync(command, CorrelationContext.Empty);
        }
    }
}
