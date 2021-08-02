using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ticket.Dtos.Customers
{
    public class CustomerDto:BaseDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Gsm { get; set; }
        public string Email { get; set; }
        public string PrimaryContactUserNameSurname { get; set; }
        public string PrimaryContactUserEmail { get; set; }
        public bool EffortApproval { get; set; }
        public int EffortApprovalLimit { get; set; }
        public string EffortApprovalTemporary { get; set; }

    }
}
