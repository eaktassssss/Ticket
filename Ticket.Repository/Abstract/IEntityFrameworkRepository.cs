using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Repository.Abstract
{
    public  interface IEntityFrameworkRepository<T>where T:class
    {
        Task<T> Insert(T entitiy);
        Task<T> Update(T entitiy);
        Task<T> GetById(object key);
        Task<T> Get(Expression<Func<T,bool>> expression);
        Task<List<T>> List(Expression<Func<T,bool>> expression);
        Task<List<T>> List();
        Task<T> Delete(T entitiy);
        Task<int> CountAsync(Expression<Func<T, bool>> expression);
    }
}
