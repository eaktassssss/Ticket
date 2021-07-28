using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Dtos.UserRoles
{
    public class UserRoleListDto
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
