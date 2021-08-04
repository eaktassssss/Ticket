using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Common.Enums;
using Ticket.Results;

namespace Ticket.MessageBroker.RabbitMQ
{
    public interface IRabbitMQMessageBroker
    {
        Task Publisher(string data, ExchangeNames exchangeName, RouteKeyNames routeKey);
        Task<DataResult<T>> Consumer<T>(ExchangeNames exchangeName, RouteKeyNames routeKey);
    }
}
