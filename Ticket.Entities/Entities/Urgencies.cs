using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ticket.Entities.Entities
{
    [Table("Urgencies")]
    public class Urgencies:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Tickets> Tickets { get; set; }
    }
}
