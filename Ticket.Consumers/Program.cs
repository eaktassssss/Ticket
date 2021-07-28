using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Ticket.Common.Enums;
using Ticket.Consumers.Elastic;
using Ticket.Consumers.Models;
using Ticket.Results;

namespace Ticket.Consumers
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceProvider serviceProvider = new ServiceCollection()
                                           .AddTransient(typeof(IElasticSearchService<>), typeof(ElasticSearchService<>))
                                           .BuildServiceProvider();
            var elastic = serviceProvider.GetService<IElasticSearchService<TicketQueueModel>>();

            try
            {
                var factory = new ConnectionFactory();
                factory.HostName = "localhost";
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: ExchangeNames.tickets.ToString(), durable: true, type: ExchangeType.Direct);
                        var queueName = channel.QueueDeclare().QueueName;
                        channel.QueueBind(queue: queueName, exchange: ExchangeNames.tickets.ToString(), routingKey: RouteKeyNames.ticket.ToString());
                        channel.BasicQos(0, 1, false);
                        var consumer = new EventingBasicConsumer(channel);
                        channel.BasicConsume(queueName, false, consumer: consumer);
                        consumer.Received += (render, argument) =>
                        {
                            string message = Encoding.UTF8.GetString(argument.Body.ToArray());
                            var model = JsonConvert.DeserializeObject<DataResult<TicketQueueModel>>(message);
                            elastic.CheckExistsAndInsert(model.Entity, IndexNames.close_ticket_data);
                            channel.BasicAck(deliveryTag: argument.DeliveryTag, false);
                        };
                        Console.ReadLine();
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
