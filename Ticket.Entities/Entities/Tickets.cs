using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Ticket.Entities.Entities
{
    [Table("Tickets")]
    public class Tickets:BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public string SubjectHeading { get; set; }
        public string SpecialCode { get; set; }
       
        
       
        [ForeignKey("TypeId")]
        public Types Types { get; set; }
        public int TypeId { get; set; }

        [ForeignKey("UrgencyId")]
        public Urgencies Urgencies { get; set; }
        public int UrgencyId { get; set; }

        [ForeignKey("ImpactId")]
        public Impacts Impacts { get; set; }
        public int ImpactId { get; set; }

        [ForeignKey("StatusId")]
        public Situations Stuations { get; set; }
        public int StatusId { get; set; }
        public string CustomerId { get; set; }
        public string Descripton { get; set; }
        public string CustomerName { get; set; }
    }
}
