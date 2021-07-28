
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.DataAccess.Abstract;
using Ticket.Dtos.UserRoles;
using Ticket.Dtos.Users;
using Ticket.Entities.Entities;
using Ticket.Helper;
using Ticket.Results;

namespace Ticket.Business.Concrete
{
    public class UserRoleManager : IUserRoleService
    {
        #region UserRoleDataAccess
        IUserRoleDataAccess _userRoleDataAccess;
        public UserRoleManager(IUserRoleDataAccess userRoleDataAccess)
        {
            _userRoleDataAccess = userRoleDataAccess;

        }
        #endregion
        public async Task<Result> Insert(UserRoleDto entity)
        {
            var response = new Result();
            try
            {

                var data = await _userRoleDataAccess.Get(x => x.UserId == entity.UserId && x.RoleId == entity.RoleId && x.IsDeleted == false).ConfigureAwait(false);
                if (data != null)
                {
                    response.IsSuccessful = false;
                    response.Message = "Bu kullanıcı için aynı yetki tanımlaması yapılmış!";
                    return response;
                }
                else
                {
                    var userRole = new UserRoles { CreatedDate = DateTime.Now, IsDeleted = false, UserId = entity.UserId, RoleId = entity.RoleId, UpdatedDate = DateTime.Now };
                    await _userRoleDataAccess.Insert(userRole).ConfigureAwait(false);
                    response.IsSuccessful = true;
                    response.Message = "Kullanıcı için yetki tanımlaması başarılı!";
                    return response;
                }

            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }
        public async Task<List<UserRoleListDto>> List()
        {
            var response = await _userRoleDataAccess.List().ConfigureAwait(false);
            return response;

        }
        public async Task<Result> Update(UserRoleDto entity)
        {
            var response = new Result();
            var model = await _userRoleDataAccess.GetById(entity.Id).ConfigureAwait(false);
            model.UserId = entity.UserId; model.RoleId = entity.RoleId; model.UpdatedDate = DateTime.Now;
            var result = await _userRoleDataAccess.Update(model).ConfigureAwait(false);
            response.IsSuccessful = result != null ? true : false;
            response.Message = response.IsSuccessful == true ? "Düzenleme İşlemi Başarılı" : "Düzenleme İşlemi Başarısız";
            return response;
        }
        public async Task<Result> Delete(int id)
        {
            var response = new Result();
            var data = await _userRoleDataAccess.GetById(id).ConfigureAwait(false);
            if (data == null)
            {
                response.IsSuccessful = false;
                response.Message = "Kayıt bulunamadı";
            }
            else
            {
                data.DeletedDate = DateTime.Now;
                data.IsDeleted = true;
                var result = await _userRoleDataAccess.Delete(data).ConfigureAwait(false);
                response.IsSuccessful = result != null ? true : false;
                response.Message = response.IsSuccessful == true ? "Silme İşlemi Başarılı" : "Silme İşlemi Başarısız";
            }
            return response;
        }
        public async Task<DataResult<UserRoleDto>> GetById(object key)
        {

            var response = new DataResult<UserRoleDto>();
            var data = await _userRoleDataAccess.GetById(key).ConfigureAwait(false);

            if (data == null)
            {
                response.Entity = null; response.IsSuccessful = false;
                return response;
            }
            var dto = new UserRoleDto { Id = data.Id, RoleId = data.RoleId, UserId = data.UserId,CreatedDate=data.CreatedDate,UpdatedDate=data.UpdatedDate,IsDeleted=data.IsDeleted };
            response.Entity = dto; response.IsSuccessful = true;
            return response;
        }
    }
}
