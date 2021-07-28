using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.DataAccess.Abstract;
using Ticket.Dtos.Customers;
using Ticket.Entities.Entities;
using Ticket.Results;
using System.Linq;
using Ticket.Core.Caching.Redis;
using Ticket.Constants;
using MongoDB.Driver;
using Ticket.Dtos.Dropdowns;

namespace Ticket.Business.Concrete
{
    public class CustomerManager : ICustomerService
    {

        #region CustomerDataAccess
        ICustomerDataAccess _customerDataAccess;
        IRedisCacheService _redisCacheService;
        public CustomerManager(ICustomerDataAccess customerDataAccess, IRedisCacheService redisCacheService)
        {
            _customerDataAccess = customerDataAccess;
            _redisCacheService = redisCacheService;
        }
        #endregion
        public async Task<Result> Delete(object id)
        {
            var response = new Result();
            var data = await _customerDataAccess.GetByIdAsync(id.ToString()).ConfigureAwait(false);
            if (data == null)
            {
                response.IsSuccessful = false;
                response.Message = "Kullanıcı bulunamadı!";
            }
            else
            {
                data.DeletedDate = DateTime.Now;
                data.IsDeleted = true;
                var result = await _customerDataAccess.DeleteAsync(data).ConfigureAwait(false);
                response.IsSuccessful = result != null ? true : false;
                response.Message = response.IsSuccessful == true ? "Silme İşlemi Başarılı" : "Silme İşlemi Başarısız";
                if (response.IsSuccessful)
                    await _redisCacheService.RemoveAsync(RedisKeyConstans.Customer);

            }
            return response;
        }

        public async Task<List<CustomerDropdownDto>> DropCustomer()
        {
            var data = await _redisCacheService.GetAsync<List<CustomerDropdownDto>>(RedisKeyConstans.Customer);
            if (data == null)
            {
                var response = await _customerDataAccess.DropCustomer().ConfigureAwait(false);
                await _redisCacheService.SetAsync(RedisKeyConstans.Customer, response);
                return response;
            }
            else
            {
                return data;
            }
        }

        public async Task<DataResult<CustomerDto>> GetById(object key)
        {
            var response = new DataResult<CustomerDto>();
            var customer = await _customerDataAccess.GetByIdAsync(key.ToString()).ConfigureAwait(false);
            if (customer == null)
            {
                response.IsSuccessful = false;
                response.Entity = null;
                return response;
            }
            var dto = new CustomerDto { Id = customer.Id, Title = customer.Title, PrimaryContactUserEmail = customer.PrimaryContactUserEmail, Gsm = customer.Gsm, Email = customer.Email, CreatedDate = customer.CreatedDate, UpdatedDate = customer.UpdatedDate, IsDeleted = customer.IsDeleted, PrimaryContactUserNameSurname = customer.PrimaryContactUserNameSurname, EffortApprovalLimit = customer.EffortApprovalLimit/60};
            dto.EffortApprovalTemporary = dto.EffortApproval == true ? "1" : "0";
            response.Entity = dto;
            response.IsSuccessful = true;
            return response;
        }

        public async Task<Result> Insert(CustomerDto dto)
        {
            var response = new Result();
            try
            {
                var customer = await _customerDataAccess.GetAsync(x => x.Email == dto.Email).ConfigureAwait(false);
                if (customer != null)
                {
                    response.IsSuccessful = false;
                    response.Message = "Email başka bir firma tarafından kullanılıyor!";
                }
                else
                {
                    var model = new Customers
                    {
                        Title = dto.Title,
                        Email = dto.Email,
                        Gsm = dto.Gsm,
                        PrimaryContactUserNameSurname = dto.PrimaryContactUserNameSurname,
                        EffortApprovalLimit = dto.EffortApprovalLimit * 60,
                        EffortApproval = dto.EffortApprovalTemporary == "1" ? true : false,
                        PrimaryContactUserEmail = dto.PrimaryContactUserEmail,
                        UpdatedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        Id = dto.Id,
                        IsDeleted = false
                    };
                    var result = await _customerDataAccess.InsertAsync(model).ConfigureAwait(false);
                    response.IsSuccessful = result != null ? true : false;
                    response.Message = response.IsSuccessful == true ? "Kayıt İşlemi Başarılı" : "Kayıt İşlemi Başarılı";
                    if (response.IsSuccessful)
                        await _redisCacheService.RemoveAsync(RedisKeyConstans.Customer);
                }
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public async Task<List<CustomerDto>> List()
        {
            var response = await _customerDataAccess.GetAll().ConfigureAwait(false);
            return response.Select(x => new CustomerDto
            {
                Title = x.Title,
                Email = x.Email,
                Gsm = x.Gsm,
                PrimaryContactUserNameSurname = x.PrimaryContactUserNameSurname,
                EffortApprovalLimit = x.EffortApprovalLimit,
                EffortApproval = x.EffortApproval,
                PrimaryContactUserEmail = x.PrimaryContactUserEmail,
                Id = x.Id,
                CreatedDate = x.CreatedDate
            }).Where(x => x.IsDeleted == false).OrderBy(x => x.Title).ToList();
        }

        public async Task<Result> Update(CustomerDto customer)
        {
            var response = new Result();
            var model = await _customerDataAccess.GetAsync(x => x.Id == customer.Id).ConfigureAwait(false);
            model.Title = customer.Title;
            model.Id = customer.Id;
            model.Gsm = customer.Gsm;
            model.Email = customer.Email;
            model.UpdatedDate = DateTime.Now;
            model.PrimaryContactUserEmail = customer.PrimaryContactUserEmail;
            model.PrimaryContactUserNameSurname = customer.PrimaryContactUserNameSurname;
            model.EffortApproval = customer.EffortApprovalTemporary == "1" ? true : false;
            model.EffortApprovalLimit = customer.EffortApprovalLimit*60;
            var result = await _customerDataAccess.UpdateAsync(model, x => x.Id == model.Id).ConfigureAwait(false);
            response.IsSuccessful = result != null ? true : false;
            response.Message = response.IsSuccessful == true ? "Düzenleme İşlemi Başarılı" : "Düzenleme İşlemi Başarısız";
            if (response.IsSuccessful)
                await _redisCacheService.RemoveAsync(RedisKeyConstans.Customer);
            return response;
        }
    }
}
