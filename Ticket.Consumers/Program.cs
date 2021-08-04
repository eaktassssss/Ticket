using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using Ticket.Common.Enums;
using Ticket.Consumers.Elastic;
using Ticket.Consumers.Models;
using Ticket.MessageBroker.RabbitMQ;
using Ticket.Results;

namespace Ticket.Consumers
{
    class Program
    {
        static void Main(string[] args)
        {

            ServiceProvider serviceProvider = new ServiceCollection()
                                           .AddTransient(typeof(IElasticSearchService<>), typeof(ElasticSearchService<>))
                                           .AddTransient(typeof(IRabbitMQMessageBroker), typeof(RabbitMQMessageBroker))
                                           .BuildServiceProvider();
            var elastic = serviceProvider.GetService<IElasticSearchService<TicketQueueModel>>();
            var rabbitmq = serviceProvider.GetService<IRabbitMQMessageBroker>();

            try
            {
                var result = rabbitmq.Consumer<TicketQueueModel>(ExchangeNames.tickets, RouteKeyNames.ticket).Result;
                elastic.CheckExistsAndInsert(result.Entity, IndexNames.close_ticket_data);
                Console.ReadLine();
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
