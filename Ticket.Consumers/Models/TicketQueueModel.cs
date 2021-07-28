using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Consumers.Models
{
    public class TicketQueueModel
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

        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
