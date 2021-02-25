using SN.CMS.Common.Domain;
using System.Threading.Tasks;

namespace SN.CMS.Common.Dispatchers.Queries
{
    public interface IQueryDispatcher
    {
        Task<TResult> QueryAsync<TResult>(IQuery<TResult> query);
    }
}
