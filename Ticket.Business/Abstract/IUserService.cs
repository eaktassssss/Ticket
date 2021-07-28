using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos;
using Ticket.Dtos.Users;
using Ticket.Entities.Entities;
using Ticket.Results;

namespace Ticket.Business.Abstract
{
    public interface IUserService
    {
        Task<Result> Insert(UserDto user);
        Task<Result> Update(UserDto entitiy);
        Task<DataResult<UserDto>> GetById(object key);
        Task<List<UserDto>> List();
        Task<Result> Delete(int id);
        Task<DataResult<List<BaseSelectedDto>>> SelectedItem();
    }
}
