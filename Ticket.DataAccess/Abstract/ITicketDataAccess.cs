using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.Dropdowns;
using Ticket.Dtos.Tickets;
using Ticket.Entities.Entities;
using Ticket.Repository.Abstract;
using Ticket.Results;

namespace Ticket.DataAccess.Abstract
{
    public interface ITicketDataAccess : IEntityFrameworkRepository<Tickets>
    {
        Task<List<DropdownDto>> DropImpacts();
        Task<List<DropdownDto>> DropTypes();
        Task<List<DropdownDto>> DropStuations();
        Task<List<DropdownDto>> DropUrgencies();
        Task<List<TicketListDto>> GetList();
        Task<DataResult<TicketQueueDto>> ClosedTicketQueueData(int key);

        Task<int> OpenTicketCount();
        Task<int> CloseTicketCount();
 
    }
}
