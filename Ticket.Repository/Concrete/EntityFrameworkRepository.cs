
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticket.Entities.Context;
using Ticket.Repository.Abstract;

namespace Ticket.Repository.Concrete
{
    public class EntityFrameworkRepository<T> : IEntityFrameworkRepository<T>
        where T : class
    {
        #region Context
        TicketContext _ticketContext;
        public EntityFrameworkRepository(TicketContext ticketContext)
        {
            _ticketContext = ticketContext;
        }
        #endregion
        public async Task<T> Insert(T entitiy)
        {
            if (entitiy == null)
                throw new ArgumentNullException(nameof(entitiy));
            else
            {
                var entry = _ticketContext.Entry(entitiy);
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                await _ticketContext.SaveChangesAsync();
                return entry.Entity;
            }
        }
        public async Task<List<T>> List(Expression<Func<T, bool>> expression)
        {
            return await _ticketContext.Set<T>().Where(expression).ToListAsync();
        }
        public async Task<List<T>> List()
        {
            return await _ticketContext.Set<T>().ToListAsync();
        }
        public async Task<T> Update(T entitiy)
        {
            try
            {
                if (entitiy == null)
                    throw new ArgumentNullException(nameof(entitiy));
                else
                {
                    var entry = _ticketContext.Entry(entitiy);
                    entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    await _ticketContext.SaveChangesAsync();
                    return entry.Entity;
                }
            }
            catch (Exception dx)
            {

                throw new Exception(dx.Message);
            }
        
        }
        public async Task<T> Delete(T entitiy)
        {

            if (entitiy == null)
                throw new ArgumentNullException(nameof(entitiy));
            {
                var entry = _ticketContext.Entry(entitiy);
                entry.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                await _ticketContext.SaveChangesAsync();
                return entry.Entity;
            }
        }
        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            return await _ticketContext.Set<T>().FirstOrDefaultAsync(expression);
        }
        public async Task<T> GetById(object key)
        {
            return await _ticketContext.Set<T>().FindAsync(key);
        }
        public async Task<int> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await _ticketContext.Set<T>().Where(expression).CountAsync();
        }
    }
}
