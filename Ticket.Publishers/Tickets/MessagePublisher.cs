using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Common.Enums;
using Ticket.DataAccess.Abstract;
using Ticket.DataAccess.Concrete;
using Ticket.Dtos.Tickets;
using Ticket.MessageBroker.RabbitMQ;
using Ticket.Results;

namespace Ticket.Publishers.Tickets
{

    public class MessagePublisher : IMessagePublisher
    {
        IRabbitMQMessageBroker _messageBroker;
        ITicketDataAccess _ticketDataAccess;
        public MessagePublisher(IRabbitMQMessageBroker messageBroker, ITicketDataAccess ticketDataAccess)
        {
            _messageBroker = messageBroker;
            _ticketDataAccess = ticketDataAccess;
        }
        public async Task SendMessageQueue(int key)
        {
            try
            {
                if (String.IsNullOrEmpty(key.ToString()))
                    throw new ArgumentNullException(nameof(key));
                else
                {
                    var data = await _ticketDataAccess.ClosedTicketQueueData(key);
                    if (data.Entity == null)
                        throw new ArgumentNullException(nameof(data.Entity));
                    else
                    {
                        var queueData = JsonConvert.SerializeObject(data);
                        await _messageBroker.Publisher(queueData, ExchangeNames.tickets, RouteKeyNames.ticket);
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
