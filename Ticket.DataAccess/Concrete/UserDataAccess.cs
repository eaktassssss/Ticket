using System;
using System.Collections.Generic;
using System.Text;
using Ticket.DataAccess.Abstract;
using Ticket.Entities.Context;
using Ticket.Entities.Entities;
using Ticket.Repository.Abstract;
using Ticket.Repository.Concrete;

namespace Ticket.DataAccess.Concrete
{
    public class UserDataAccess:EntityFrameworkRepository<Users>,IUserDataAccess
    {
        TicketContext _ticketContext;
        public UserDataAccess(TicketContext ticketContext):base(ticketContext)
        {
            _ticketContext = ticketContext;
        }
    }
}
