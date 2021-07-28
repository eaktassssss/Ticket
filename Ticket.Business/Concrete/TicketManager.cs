using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.Constants;
using Ticket.Core.Caching.Redis;
using Ticket.DataAccess.Abstract;
using Ticket.Dtos.Dropdowns;
using Ticket.Dtos.Tickets;
using Ticket.Entities.Entities;
using Ticket.Publishers.Tickets;
using Ticket.Results;

namespace Ticket.Business.Concrete
{
    public class TicketManager : ITicketService
    {
        #region TicketDataAccess
        ICustomerDataAccess _customerDataAccess;
        ITicketDataAccess _ticketDataAccess;
        IRedisCacheService _redisCacheService;
        IMessagePublisher _ticketMessagePublisher;
        public TicketManager(ICustomerDataAccess customerDataAccess,ITicketDataAccess ticketDataAccess, IRedisCacheService redisCacheService, IMessagePublisher ticketMessagePublisher)
        {
            _redisCacheService = redisCacheService;
            _ticketMessagePublisher = ticketMessagePublisher;
            _ticketDataAccess = ticketDataAccess;
            _customerDataAccess = customerDataAccess;
        }
        #endregion
        public async Task<Result> Insert(TicketDto ticket)
        {
            var response = new Result();
            try
            {
                Customers customer = await _customerDataAccess.GetAsync(x => x.Id == ticket.CustomerId);
                if (customer !=null)
                {
                    var model = new Tickets
                    {
                        ImpactId = ticket.ImpactId,
                        CustomerId = ticket.CustomerId,
                        CustomerName = customer.Title,
                        TypeId = ticket.TypeId,
                        UrgencyId = ticket.UrgencyId,
                        Descripton = ticket.Descripton,
                        SpecialCode = Guid.NewGuid().ToString().Substring(0, 10),
                        SubjectHeading = ticket.SubjectHeading,
                        UpdatedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        IsDeleted = false,
                        StatusId = 6,
                    };
                    var result = await _ticketDataAccess.Insert(model).ConfigureAwait(false);
                    response.IsSuccessful = result != null ? true : false;
                    response.Message = response.IsSuccessful == true ? "Kayıt İşlemi Başarılı" : "Kayıt İşlemi Başarısız";
                    return response;
                }
                else
                {
                    response.IsSuccessful = false;
                    response.Message ="Kayıt İşlemi Başarısız. Müşteri bilgisi bulunamadı";
                    return response;
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }
        public async Task<List<TicketListDto>> List()
        {
            var response = await _ticketDataAccess.GetList().ConfigureAwait(false);
            return response;
        }
        public async Task<Result> Update(TicketDto ticket)
        {
            bool publishQueue = false;
            var response = new Result();
            var model = await _ticketDataAccess.GetById(ticket.Id).ConfigureAwait(false);
            var customer = await _customerDataAccess.GetAsync(x=> x.Id==ticket.CustomerId);
            if (ticket.StatusId == 6 && model.StatusId != ticket.StatusId)
            {
                publishQueue = true;
            }
            model.ImpactId = ticket.ImpactId; model.UrgencyId = ticket.UrgencyId; model.StatusId = ticket.StatusId; model.TypeId = ticket.TypeId; model.SubjectHeading = ticket.SubjectHeading; model.Descripton = ticket.Descripton; model.SpecialCode = model.SpecialCode; model.UpdatedDate = DateTime.Now;
            model.CustomerId = customer.Id;model.CustomerName = customer.Title;
            var result = await _ticketDataAccess.Update(model).ConfigureAwait(false);
            response.IsSuccessful = result != null ? true : false;
            response.Message = response.IsSuccessful == true ? "Düzenleme İşlemi Başarılı" : "Düzenleme İşlemi Başarısız";
            if (publishQueue&& response.IsSuccessful)
            {
                await _ticketMessagePublisher.SendMessageQueue(model.Id);
            }
          
            return response;
        }
        public async Task<Result> Delete(int id)
        {
            var response = new Result();
            var data = await _ticketDataAccess.GetById(id).ConfigureAwait(false);
            if (data == null)
            {
                response.IsSuccessful = false;
                response.Message = "Kullanıcı bulunamadı";
            }
            else
            {
                data.DeletedDate = DateTime.Now;
                data.IsDeleted = true;
                var result = await _ticketDataAccess.Delete(data).ConfigureAwait(false);
                response.IsSuccessful = result != null ? true : false;
                response.Message = response.IsSuccessful == true ? "Silme İşlemi Başarılı" : "Silme İşlemi Başarısız";
            }
            return response;
        }
        public async Task<DataResult<TicketDto>> GetById(object key)
        {

            var response = new DataResult<TicketDto>();
            var ticket = await _ticketDataAccess.GetById(key).ConfigureAwait(false);
            if (ticket == null)
            {
                response.Entity = null; response.IsSuccessful = false;
                return response;
            }
            var dto = new TicketDto { Id = ticket.Id, ImpactId = ticket.ImpactId, TypeId = ticket.TypeId, StatusId = ticket.StatusId, IsDeleted = ticket.IsDeleted, CreatedDate = ticket.CreatedDate, UpdatedDate = ticket.UpdatedDate, CustomerId = ticket.CustomerId, UrgencyId = ticket.UrgencyId, SpecialCode = ticket.SpecialCode, SubjectHeading = ticket.SubjectHeading, Descripton = ticket.Descripton };
            response.Entity = dto; response.IsSuccessful = true;
            return response;
        }
        public async Task<List<DropdownDto>> DropImpacts()
        {
            var data = await _redisCacheService.GetAsync<List<DropdownDto>>(RedisKeyConstans.Impacts);
            if (data == null)
            {
                var response = await _ticketDataAccess.DropImpacts().ConfigureAwait(false);
                await _redisCacheService.SetAsync(RedisKeyConstans.Impacts, response);
                return response;
            }
            else
            {
                return data;
            }
        }
        public async Task<List<DropdownDto>> DropStuations()
        {

            var data = await _redisCacheService.GetAsync<List<DropdownDto>>(RedisKeyConstans.Stuations);
            if (data == null)
            {
                var response = await _ticketDataAccess.DropStuations().ConfigureAwait(false);
                await _redisCacheService.SetAsync(RedisKeyConstans.Stuations, response);
                return response;
            }
            else
            {
                return data;
            }
        }
        public async Task<List<DropdownDto>> DropTypes()
        {
            var data = await _redisCacheService.GetAsync<List<DropdownDto>>(RedisKeyConstans.Types);
            if (data == null)
            {
                var response = await _ticketDataAccess.DropTypes().ConfigureAwait(false);
                await _redisCacheService.SetAsync(RedisKeyConstans.Types, response);
                return response;
            }
            else
            {
                return data;
            }

        }
        public async Task<List<DropdownDto>> DropUrgencies()
        {
            var data = await _redisCacheService.GetAsync<List<DropdownDto>>(RedisKeyConstans.Urgencies);
            if (data == null)
            {
                var response = await _ticketDataAccess.DropUrgencies().ConfigureAwait(false);
                await _redisCacheService.SetAsync(RedisKeyConstans.Urgencies, response);
                return response;
            }
            else
            {
                return data;
            }
        }
        public Task<int> CountAsync()
        {
            return _ticketDataAccess.CountAsync(x => x.IsDeleted == false);
        }

        public async Task<int> OpenTicketCount()
        {
            return await _ticketDataAccess.OpenTicketCount();
        }

        public async Task<int> CloseTicketCount()
        {
            return await _ticketDataAccess.CloseTicketCount();
        }
    }
}
