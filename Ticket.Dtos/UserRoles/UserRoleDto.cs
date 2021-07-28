using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Dtos.UserRoles
{
    public  class UserRoleDto:BaseDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
