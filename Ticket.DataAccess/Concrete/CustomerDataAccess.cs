using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Configuration.Mongo;
using Ticket.DataAccess.Abstract;
using Ticket.Dtos.Dropdowns;
using Ticket.Entities.Context;
using Ticket.Entities.Entities;
using Ticket.Repository.Concrete;

namespace Ticket.DataAccess.Concrete
{
    public class CustomerDataAccess : MongoRepository<Customers>, ICustomerDataAccess
    {
        public CustomerDataAccess(IMongoConfiguration settings) : base(settings)
        {
        }
        public async Task<List<CustomerDropdownDto>> DropCustomer()
        {
            var customers = GetAll().Result.Select(x=> new CustomerDropdownDto {
                Id = x.Id,
                Name = x.Title,
                IsDeleted = x.IsDeleted

            }).Where(x=> x.IsDeleted==false).ToList();
            return await Task.FromResult(customers);
        }

    }
}
