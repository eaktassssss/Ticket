using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Configuration.Elastic
{
    public interface IElasticConfiguration
    {
          string ConnectionString { get; set; }
          string UserName { get; set; }
          string Password { get; set; }
    }
}
