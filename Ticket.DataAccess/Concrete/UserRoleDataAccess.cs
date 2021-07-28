using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Ticket.DataAccess.Abstract;
using Ticket.Entities.Context;
using Ticket.Entities.Entities;
using Ticket.Repository.Concrete;
using System.Linq;
using Ticket.Dtos.UserRoles;

namespace Ticket.DataAccess.Concrete
{
    public class UserRoleDataAccess : EntityFrameworkRepository<UserRoles>, IUserRoleDataAccess
    {
        TicketContext _ticketContext;
        public UserRoleDataAccess(TicketContext ticketContext) : base(ticketContext)
        {
            _ticketContext = ticketContext;

        }
        public  Task<List<UserRoleListDto>> List()
        {
            var response = (from ur in _ticketContext.UserRoles join r in _ticketContext.Roles
                           on ur.RoleId equals r.Id join u in _ticketContext.Users
                           on ur.UserId equals u.Id select new UserRoleListDto
                           { 
                             Id=ur.Id,UserName=String.Concat(u.Name," ",u.Surname),RoleName=r.Name,CreatedDate=ur.CreatedDate,
                               IsDeleted=ur.IsDeleted

                           }).Where(x=> x.IsDeleted==false).ToList();
            return Task.FromResult(response);

        }
    }
}
