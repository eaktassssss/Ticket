using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Configuration.Mongo
{
    public class MongoConfiguration: IMongoConfiguration
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
