using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Ticket.DataAccess.Abstract;
using Ticket.Entities.Context;
using Ticket.Entities.Entities;
using Ticket.Repository.Concrete;
using System.Linq;
using Ticket.Dtos.Tickets;
using Ticket.Dtos.Dropdowns;

using Ticket.Repository.Abstract;
using Ticket.Results;

namespace Ticket.DataAccess.Concrete
{
    public class TicketDataAccess : EntityFrameworkRepository<Tickets>, ITicketDataAccess
    {
        TicketContext _ticketContext;
        ICustomerDataAccess _customerDataAccess;
        public TicketDataAccess(TicketContext ticketContext, ICustomerDataAccess customerDataAccess) : base(ticketContext)
        {
            _ticketContext = ticketContext;
            _customerDataAccess = customerDataAccess;

        }

        public async Task<List<DropdownDto>> DropImpacts()
        {
            var response = _ticketContext.Impacts.Select(x => new DropdownDto
            {
                Id = x.Id,
                Name = x.Name,
                IsDeleted = x.IsDeleted

            }).Where(x => x.IsDeleted == false).ToList();
            return await Task.FromResult(response);
        }
        public async Task<List<TicketListDto>> GetList()
        {
            var response = (from ticket in _ticketContext.Tickets
                            join urgency in _ticketContext.Urgencies
                            on ticket.UrgencyId equals urgency.Id
                            join impact in _ticketContext.Impacts
                            on ticket.ImpactId equals impact.Id
                            join status in _ticketContext.Situations on
                            ticket.StatusId equals status.Id
                            join type in _ticketContext.Types on
                            ticket.TypeId equals type.Id
                            select new TicketListDto
                            {
                                ImpactName = impact.Name,
                                SpecialCode = ticket.SpecialCode,
                                Descripton = ticket.Descripton,
                                SubjectHeading = ticket.SubjectHeading,
                                UrgencyName = urgency.Name,
                                StatusName = status.Name,
                                TypeName = type.Name,
                                CustomerName=ticket.CustomerName,
                                Id = ticket.Id,
                                CustomerId = ticket.CustomerId,
                                IsDeleted = ticket.IsDeleted,
                                CreatedDate = ticket.CreatedDate,
                                UpdatedDate = ticket.UpdatedDate
                            }).Where(x => x.IsDeleted == false).ToList();
             
            return await Task.FromResult(response);
        }
        public async Task<List<DropdownDto>> DropStuations()
        {
            var response = _ticketContext.Situations.Select(x => new DropdownDto
            {
                Id = x.Id,
                Name = x.Name,
                IsDeleted = x.IsDeleted

            }).Where(x => x.IsDeleted == false).ToList();
            return await Task.FromResult(response);
        }
        public async Task<List<DropdownDto>> DropTypes()
        {
            var response = _ticketContext.Types.Select(x => new DropdownDto
            {
                Id = x.Id,
                Name = x.Name,
                IsDeleted = x.IsDeleted

            }).Where(x => x.IsDeleted == false).ToList();
            return await Task.FromResult(response);
        }
        public async Task<List<DropdownDto>> DropUrgencies()
        {
            var response = _ticketContext.Urgencies.Select(x => new DropdownDto
            {
                Id = x.Id,
                Name = x.Name,
                IsDeleted = x.IsDeleted

            }).Where(x => x.IsDeleted == false).ToList();
            return await Task.FromResult(response);
        }

        public async Task<DataResult<TicketQueueDto>> ClosedTicketQueueData(int key)
        {
            var response = new DataResult<TicketQueueDto>();
            var result = (from ticket in _ticketContext.Tickets
                          join urgency in _ticketContext.Urgencies
                          on ticket.UrgencyId equals urgency.Id
                          join impact in _ticketContext.Impacts
                          on ticket.ImpactId equals impact.Id
                          join status in _ticketContext.Situations on
                          ticket.StatusId equals status.Id
                          join type in _ticketContext.Types on
                          ticket.TypeId equals type.Id
                          select new TicketQueueDto
                          {
                              ImpactName = impact.Name,
                              SpecialCode = ticket.SpecialCode,
                              Descripton = ticket.Descripton,
                              SubjectHeading = ticket.SubjectHeading,
                              UrgencyName = urgency.Name,
                              StatusName = status.Name,
                              TypeName = type.Name,
                              Id = ticket.Id,
                              CustomerId = ticket.CustomerId,
                              IsDeleted = ticket.IsDeleted,
                              CreatedDate = ticket.CreatedDate,
                              UpdatedDate = ticket.UpdatedDate
                          }).Where(x => x.IsDeleted == false).FirstOrDefault(t => t.Id == (int)key);
            var customerName = await _customerDataAccess.GetByIdAsync(result.CustomerId);
            result.CustomerName = customerName.Title;
            response.Entity = result;
            response.IsSuccessful = true;
            return await Task.FromResult(response);
        }

        public async Task<int> OpenTicketCount()
        {
            return await CountAsync(x => x.IsDeleted == false && x.StatusId != 6);
        }

        public async Task<int> CloseTicketCount()
        {
            return await CountAsync(x => x.IsDeleted == false && x.StatusId == 6);
        }
    }
}
