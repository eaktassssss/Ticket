using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Configuration.Elastic
{
    public class ElasticConfiguration: IElasticConfiguration
    {
        public string ConnectionString { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
