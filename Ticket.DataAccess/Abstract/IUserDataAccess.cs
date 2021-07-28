using System;
using System.Collections.Generic;
using System.Text;
using Ticket.Entities.Entities;
using Ticket.Repository.Abstract;

namespace Ticket.DataAccess.Abstract
{
    public interface IUserDataAccess:IEntityFrameworkRepository<Users>
    {
       
    }
}
