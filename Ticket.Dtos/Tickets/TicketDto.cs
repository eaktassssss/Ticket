using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Dtos.Tickets
{
    public class TicketDto:BaseDto
    {
        public int Id { get; set; }
        public string SubjectHeading { get; set; }
        public string SpecialCode { get; set; }
        public int TypeId { get; set; }
        public int UrgencyId { get; set; }
        public int ImpactId { get; set; }
        public int StatusId { get; set; }
        public string CustomerId { get; set; }
        public string Descripton { get; set; }
    }
}
