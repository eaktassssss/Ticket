using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ticket.Entities.Entities
{
    [Table("Types")]
    public class Types:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Tickets> Tickets { get; set; }
    }
}
