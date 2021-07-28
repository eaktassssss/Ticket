using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ticket.Dtos.Roles
{
    public class RoleDto: BaseDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Zorunlu alan")]
        public string Name { get; set; }
    }
}
