using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.Dropdowns;
using Ticket.Dtos.Tickets;
using Ticket.Results;

namespace Ticket.Business.Abstract
{
    public interface ITicketService
    {
        Task<Result> Insert(TicketDto user);
        Task<Result> Update(TicketDto entitiy);
        Task<DataResult<TicketDto>> GetById(object key);
        Task<List<TicketListDto>> List();
        Task<Result> Delete(int id);
        Task<List<DropdownDto>> DropImpacts();
        Task<List<DropdownDto>> DropTypes();
        Task<List<DropdownDto>> DropStuations();
        Task<List<DropdownDto>> DropUrgencies();
   
        Task<int> CountAsync();
        Task<int> OpenTicketCount();
        Task<int> CloseTicketCount();
    }
}
