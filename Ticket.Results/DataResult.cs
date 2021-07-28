using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Results
{
    public class DataResult<T>:Result
    {
        public T Entity { get; set; }
    }
}
