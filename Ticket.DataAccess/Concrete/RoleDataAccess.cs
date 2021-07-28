using System;
using System.Collections.Generic;
using System.Text;
using Ticket.DataAccess.Abstract;
using Ticket.Entities.Context;
using Ticket.Entities.Entities;
using Ticket.Repository.Concrete;

namespace Ticket.DataAccess.Concrete
{
    public class RoleDataAccess : EntityFrameworkRepository<Roles>, IRoleDataAccess
    {
        TicketContext _ticketContext;
        public RoleDataAccess(TicketContext ticketContext) : base(ticketContext)
        {
            _ticketContext = ticketContext;
        }


    }
}
