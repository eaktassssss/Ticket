using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ticket.Consumers.Configuration
{
    public static class ElasticConfig
    {
        public static string ConnectionString { get { return "http://localhost:9200"};}
        public static string UserName { get { return "elastic"};}
        public static string Password { get { return "pwd123456**"};}
    }
}
