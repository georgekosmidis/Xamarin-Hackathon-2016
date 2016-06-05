using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using XDevMessaging.Infrastructure.Tables;

namespace XDevMessaging.Infrastructure.Repositories
{
    public interface IRepository : IDisposable
    {
        Task UpdateAsync<T>(T item);
        Task InsertAsync<T>(T item);
        Task DeleteAsync<T>(T item);
        Task<IEnumerable<T>> GetAsync<T>(Expression<Func<T, bool>> predicate);
    }
}
