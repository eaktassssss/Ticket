using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos;
using Ticket.Dtos.Roles;
using Ticket.Results;

namespace Ticket.Business.Abstract
{
    public interface IRoleService
    {
        Task<Result> Insert(RoleDto entitiy);
        Task<Result> Update(RoleDto entitiy);
        Task<DataResult<RoleDto>> GetById(object key);
        Task<List<RoleDto>> List();
        Task<Result> Delete(int id);
        Task<DataResult<List<BaseSelectedDto>>> SelectedItem();
    }
}
