
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Ticket.Business.Abstract;
using Ticket.DataAccess.Abstract;
using Ticket.Dtos;
using Ticket.Dtos.Users;
using Ticket.Entities.Entities;
using Ticket.Helper;
using Ticket.Results;

namespace Ticket.Business.Concrete
{
    public class UserManager : IUserService
    {
        #region UserDataAccess
        IUserDataAccess _userDataAccess;
        IHashService _hashService;
        IUserRoleDataAccess _userRoleDataAccess;
        public UserManager(IUserRoleDataAccess userRoleDataAccess, IUserDataAccess userDataAccess, IHashService hashService)
        {
            _userDataAccess = userDataAccess;
            _hashService = hashService;
            _userRoleDataAccess = userRoleDataAccess;
        }
        #endregion
        public async Task<Result> Insert(UserDto user)
        {

            var response = new Result();
            try
            {
                var email = await _userDataAccess.Get(x => x.Email.ToLower().Trim() == user.Email.ToLower().Trim()).ConfigureAwait(false);
                var gsm = await _userDataAccess.Get(x => x.Gsm.Trim() == user.Gsm.Trim());
                var identity = await _userDataAccess.Get(x => x.IdentityNumber.Trim() == user.IdentityNumber.Trim()).ConfigureAwait(false);

                if (email != null)
                {
                    response.IsSuccessful = false;
                    response.Message = "Email başka kullanıcı tarafından kullanılıyor";
                }
                else if (gsm != null)
                {
                    response.IsSuccessful = false;
                    response.Message = "Gsm başka kullanıcı tarafından kullanılıyor";
                }
                else if (identity != null)
                {
                    response.IsSuccessful = false;
                    response.Message = "Tc başka kullanıcı tarafından kullanılıyor";
                }
                else
                {
                    var model = new Users
                    {
                        Name = user.Name,
                        Gsm = user.Gsm,
                        Email = user.Email,
                        IsDeleted = false,
                        Surname = user.Surname,
                        IdentityNumber = user.IdentityNumber,
                        UserName = user.Email.Split("@")[0],
                        UpdatedDate = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        PasswordHash = _hashService.CreateHash(user.Password, _hashService.CreateSalt())
                    };
                    var result = await _userDataAccess.Insert(model).ConfigureAwait(false);
                    response.IsSuccessful = result != null ? true : false;
                    response.Message = response.IsSuccessful == true ? "Kayıt İşlemi Başarılı" : "Kayıt İşlemi Başarısız";
                }
                return response;
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }


        }
        public async Task<List<UserDto>> List()
        {
            var response = await _userDataAccess.List(x => x.IsDeleted == false).ConfigureAwait(false);
            return response.Select(x => new UserDto()
            {
                Name = x.Name,
                Surname = x.Surname,
                Email = x.Email,
                IdentityNumber = x.IdentityNumber,
                CreatedDate = x.CreatedDate,
                UpdatedDate=x.UpdatedDate,
                Gsm = x.Gsm,
                Id = x.Id
            }).OrderByDescending(x=> x.UpdatedDate).ToList();
        }
        public async Task<Result> Update(UserDto user)
        {
            try
            {
                var response = new Result();
                var model = await _userDataAccess.GetById(user.Id).ConfigureAwait(false);
                if (!String.IsNullOrEmpty(user.Password))
                    model.PasswordHash = _hashService.CreateHash(user.Password, _hashService.CreateSalt());
                model.Email = user.Email; model.Surname = user.Surname; model.Gsm = user.Gsm; model.Name = user.Name; model.IdentityNumber = user.IdentityNumber; model.UpdatedDate = DateTime.Now;
                var result = await _userDataAccess.Update(model).ConfigureAwait(false);
                response.IsSuccessful = result != null ? true : false;
                response.Message = response.IsSuccessful == true ? "Düzenleme İşlemi Başarılı" : "Düzenleme İşlemi Başarısız";
                return response;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
        public async Task<Result> Delete(int id)
        {
            var response = new Result();
            try
            {
                var data = await _userDataAccess.GetById(id).ConfigureAwait(false);
                if (data == null)
                {
                    response.IsSuccessful = false;
                    response.Message = "Kullanıcı bulunamadı";
                }
                else
                {
                    data.DeletedDate = DateTime.Now;
                    data.IsDeleted = true;
                    var result = await _userDataAccess.Delete(data).ConfigureAwait(false);

                    response.IsSuccessful = result != null ? true : false;
                    response.Message = response.IsSuccessful == true ? "Silme İşlemi Başarılı" : "Silme İşlemi Başarısız";
                }
            }
            catch (Exception exception)
            {

                throw new Exception(exception.Message);
            }

            return response;
        }
        public async Task<DataResult<UserDto>> GetById(object key)
        {

            var response = new DataResult<UserDto>();
            var user = await _userDataAccess.GetById(key).ConfigureAwait(false);

            if (user == null)
            {
                response.Entity = null; response.IsSuccessful = false;
                return response;
            }
            var dto = new UserDto { Id = user.Id, Name = user.Name, Surname = user.Surname, IdentityNumber = user.IdentityNumber, Email = user.Email, Gsm = user.Gsm};
            response.Entity = dto; response.IsSuccessful = true;
            return response;
        }

        public async Task<DataResult<List<BaseSelectedDto>>> SelectedItem()
        {
            var response = new DataResult<List<BaseSelectedDto>>();
            var users = await _userDataAccess.List().ConfigureAwait(false);
            response.Entity = users.Select(x => new BaseSelectedDto
            {
                Value = x.Id,
                Text = string.Concat(x.Name, " ", x.Surname)
            }).ToList();
            return response;
        }
    }
}
