using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.Dtos.UserRoles;
using Ticket.Entities.Entities;
using Ticket.Repository.Abstract;

namespace Ticket.DataAccess.Abstract
{
    public interface IUserRoleDataAccess:IEntityFrameworkRepository<UserRoles>
    {
        Task<List<UserRoleListDto>> List();
    }
}
