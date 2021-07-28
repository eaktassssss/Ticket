using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.Tickets;
using Ticket.Results;

namespace Ticket.Publishers.Tickets
{
    public interface IMessagePublisher
    {
        Task SendMessageQueue(int key);
    }
}
