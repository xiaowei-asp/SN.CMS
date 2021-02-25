using SN.CMS.Common.Domain;
using System.Threading.Tasks;

namespace SN.CMS.Common.Handlers
{
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        Task<TResult> HandleAsync(TQuery query);
    }
}
