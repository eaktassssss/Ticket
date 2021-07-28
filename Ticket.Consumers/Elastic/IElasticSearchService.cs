using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Common.Enums;

namespace Ticket.Consumers.Elastic
{
    public interface IElasticSearchService<T>
          where T : class, new()
    {
        Task<bool> CheckExistsAndInsert(T model, IndexNames indexName);
    }
}
