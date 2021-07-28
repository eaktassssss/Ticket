using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.Dropdowns;
using Ticket.Entities.Entities;
using Ticket.Repository.Abstract;

namespace Ticket.DataAccess.Abstract
{
    public interface ICustomerDataAccess:IMongoRepository<Customers,string>
    {
        Task<List<CustomerDropdownDto>> DropCustomer();
    }
}
