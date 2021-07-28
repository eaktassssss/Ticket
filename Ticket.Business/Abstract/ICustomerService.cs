using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.Customers;
using Ticket.Dtos.Dropdowns;
using Ticket.Results;

namespace Ticket.Business.Abstract
{
    public interface ICustomerService
    {
        Task<Result> Insert(CustomerDto user);
        Task<Result> Update(CustomerDto user);
        Task<DataResult<CustomerDto>> GetById(object key);
        Task<List<CustomerDto>> List();
        Task<Result> Delete(object id);
        Task<List<CustomerDropdownDto>> DropCustomer();
    }
}
