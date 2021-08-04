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



        public async Task<DataResult<T>> Consumer<T>(ExchangeNames exchangeName, RouteKeyNames routeKey)
        {
            DataResult<T> response = new DataResult<T>();
            try
            {
                var factory = new ConnectionFactory();
                factory.HostName = "localhost";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: exchangeName.ToString(), durable: true, type: ExchangeType.Direct);
                        var queueName = channel.QueueDeclare().QueueName;
                        channel.QueueBind(queue: queueName, exchange: exchangeName.ToString(), routingKey: routeKey.ToString());
                        channel.BasicQos(0, 1, false);
                        var consumer = new EventingBasicConsumer(channel);
                        channel.BasicConsume(queueName, false, consumer: consumer);
                        consumer.Received += (render, argument) =>
                        {
                            string message = Encoding.UTF8.GetString(argument.Body.ToArray());
                            response = JsonConvert.DeserializeObject<DataResult<T>>(message);
                            channel.BasicAck(deliveryTag: argument.DeliveryTag, false);
                        };
                    }
                    return response;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

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
                        channel.ExchangeDeclare(exchange: exchangeName.ToString(), durable: true, type: ExchangeType.Direct);
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
