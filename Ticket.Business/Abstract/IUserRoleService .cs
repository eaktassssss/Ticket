using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.Roles;
using Ticket.Dtos.UserRoles;
using Ticket.Results;

namespace Ticket.Business.Abstract
{
    public interface IUserRoleService
    {
        Task<Result> Insert(UserRoleDto entitiy);
        Task<Result> Update(UserRoleDto entitiy);
        Task<DataResult<UserRoleDto>> GetById(object key);
        Task<List<UserRoleListDto>> List();
        Task<Result> Delete(int id);
    }
}
