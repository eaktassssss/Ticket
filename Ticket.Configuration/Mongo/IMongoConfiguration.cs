using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Configuration.Mongo
{
    public interface IMongoConfiguration
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
