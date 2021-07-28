using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Ticket.Entities.Common.Abstract;

namespace Ticket.Entities.Entities
{

  
    public  class Customers:MongoBaseEntity
    {
        public string Title { get; set; }
        public string Gsm { get; set; }
        public string Email { get; set; }
        public string PrimaryContactUserNameSurname { get; set; }
        public string PrimaryContactUserEmail { get; set; }
        public bool EffortApproval { get; set; }
        public int EffortApprovalLimit { get; set; }
    }
}
