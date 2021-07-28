using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ticket.Entities.Entities
{
    [Table("Users")]
    public class Users: BaseEntity
    {

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
        public string Gsm { get; set; }
        public string PasswordHash { get; set; }
        public string UserName { get; set; }
    }
}
