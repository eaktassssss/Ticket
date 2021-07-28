using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Dtos.Tickets
{
   public  class TicketListDto:BaseDto
    {
        public int Id { get; set; }
        public string SubjectHeading { get; set; }
        public string SpecialCode { get; set; }
        public string TypeName { get; set; }
        public string UrgencyName { get; set; }
        public string ImpactName { get; set; }
        public string StatusName { get; set; }
        public string CustomerName { get; set; }
        public string Descripton { get; set; }
        public string CustomerId { get; set; }
    }
}
