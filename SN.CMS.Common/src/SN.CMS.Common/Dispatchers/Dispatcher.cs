using SN.CMS.Common.Dispatchers.Commands;
using SN.CMS.Common.Dispatchers.Queries;
using SN.CMS.Common.Domain;
using System.Threading.Tasks;

namespace SN.CMS.Common.Dispatchers
{
    public class Dispatcher : IDispatcher
    {

        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IQueryDispatcher _queryDispatcher;

        public Dispatcher(ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
        }
        public async Task<TResult> QueryAsync<TResult>(IQuery<TResult> query)
        {
            var result = await _queryDispatcher.QueryAsync<TResult>(query);
            return result;
        }

        public async Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _commandDispatcher.SendAsync(command);
        }
    }
}
