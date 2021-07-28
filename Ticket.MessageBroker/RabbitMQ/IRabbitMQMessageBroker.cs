using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Common.Enums;

namespace Ticket.MessageBroker.RabbitMQ
{
    public interface IRabbitMQMessageBroker
    {
        Task Publisher(string data, ExchangeNames exchangeName, RouteKeyNames routeKey);
    }
}
