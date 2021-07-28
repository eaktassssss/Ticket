using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ticket.Entities.Entities
{
    [Table("UserRoles")]
    public class UserRoles:BaseEntity
    {

        [Key]
        public int Id { get; set; }
        public int RoleId { get; set; }
        [ForeignKey("RoleId")]
        public Roles Roles { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public Users Users { get; set; }

    }
}
