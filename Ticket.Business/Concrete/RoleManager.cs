
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticket.Business.Abstract;
using Ticket.DataAccess.Abstract;
using Ticket.Dtos;
using Ticket.Dtos.Roles;
using Ticket.Dtos.Users;
using Ticket.Entities.Entities;
using Ticket.Helper;
using Ticket.Results;

namespace Ticket.Business.Concrete
{
    public class RoleManager : IRoleService
    {
        #region UserDataAccess
        IRoleDataAccess _roleDataAccess;
        public RoleManager(IRoleDataAccess userDataAccess)
        {
            _roleDataAccess = userDataAccess;
        }
        #endregion
        public async Task<Result> Insert(RoleDto dto)
        {
            var response = new Result();
            try
            {
                var role = await _roleDataAccess.Get(x => x.Name.ToLower().Trim() == dto.Name.ToLower().Trim()).ConfigureAwait(false);
                if (role != null)
                {
                    response.IsSuccessful = false;
                    response.Message = "Yetki daha önce tanımlanmış";
                }
                else
                {
                    var model = new Roles
                    {
                        Name = dto.Name,
                    };
                    var result = await _roleDataAccess.Insert(model).ConfigureAwait(false);
                    response.IsSuccessful = result != null ? true : false;
                    response.Message = response.IsSuccessful == true ? "Kayıt İşlemi Başarılı" : "Kayıt İşlemi Başarılı";
                }
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }

        }

        public async Task<List<RoleDto>> List()
        {
            var response = await _roleDataAccess.List(x => x.IsDeleted == false);
            return response.Select(x => new RoleDto()
            {
                Name = x.Name,
                Id = x.Id,
                CreatedDate = x.CreatedDate
            }).ToList();
        }
        public async Task<Result> Update(RoleDto role)
        {
            var response = new Result();
            var model = await _roleDataAccess.GetById(role.Id).ConfigureAwait(false);
            model.Name = role.Name;
            var result = await _roleDataAccess.Update(model).ConfigureAwait(false);
            response.IsSuccessful = result != null ? true : false;
            response.Message = response.IsSuccessful == true ? "Düzenleme İşlemi Başarılı" : "Düzenleme İşlemi Başarısız";
            return response;
        }
        public async Task<Result> Delete(int id)
        {
            var response = new Result();
            var data = await _roleDataAccess.GetById(id);
            if (data == null)
            {
                response.IsSuccessful = false;
                response.Message = "Kullanıcı bulunamadı!";
            }
            else
            {
                data.DeletedDate = DateTime.Now;
                data.IsDeleted = true;
                var result = await _roleDataAccess.Delete(data);
                response.IsSuccessful = result != null ? true : false;
                response.Message = response.IsSuccessful == true ? "Silme İşlemi Başarılı" : "Silme İşlemi Başarısız";

            }
            return response;
        }
        public async Task<DataResult<RoleDto>> GetById(object key)
        {
            var response = new DataResult<RoleDto>();
            var user = await _roleDataAccess.GetById(key).ConfigureAwait(false);
            if (user==null)
            {
                response.Entity = null;response.IsSuccessful = false;
                return response;
            }
            var dto = new RoleDto { Id = user.Id, Name = user.Name };
            response.Entity = dto; response.IsSuccessful = true;
            return response;
        }

        public async Task<DataResult<List<BaseSelectedDto>>> SelectedItem()
        {
            var response = new DataResult<List<BaseSelectedDto>>();
            var users = await _roleDataAccess.List().ConfigureAwait(false);
            response.Entity = users.Select(x => new BaseSelectedDto
            {
                Value = x.Id,
                Text =x.Name

            }).ToList();
            return response;
        }
    }
}
