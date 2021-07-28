using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ticket.Entities.Entities
{

    [Table("Roles")]
    public class Roles : BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
