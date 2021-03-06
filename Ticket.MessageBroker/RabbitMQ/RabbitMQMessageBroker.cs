using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Common.Enums;
using Ticket.Results;

namespace Ticket.MessageBroker.RabbitMQ
{
    public class RabbitMQMessageBroker : IRabbitMQMessageBroker
    {



       

        public async Task Publisher(string data, ExchangeNames exchangeName, RouteKeyNames routeKey)
        {

            try
            {
                var factory = new ConnectionFactory();
                factory.HostName = "localhost";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: exchangeName.ToString(), durable: true, autoDelete: false, type: ExchangeType.Direct);
                        var propertis = channel.CreateBasicProperties();
                        propertis.Persistent = true;
                        var body = Encoding.UTF8.GetBytes(data);
                        channel.BasicPublish(exchangeName.ToString(), routingKey: routeKey.ToString(), propertis, body: body);
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
