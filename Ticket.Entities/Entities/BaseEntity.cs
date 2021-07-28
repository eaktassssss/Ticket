using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Entities.Entities
{
    public class BaseEntity
    {
        public bool IsDeleted { get; set; } = false;
        public DateTime? DeletedDate { get; set; }
        public DateTime UpdatedDate { get; set; } = DateTime.Now;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
