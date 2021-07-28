using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Dtos
{
    public class BaseDto
    {
        public bool IsDeleted { get; set; }
        public DateTime DeletedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime CreatedDate { get; set; }
 
    }
}
