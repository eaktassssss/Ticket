using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ticket.Dtos.Customers
{
    public class CustomerDto:BaseDto
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
             [Phone(ErrorMessage = "Format dışı gsm")]
        public string Gsm { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        [EmailAddress(ErrorMessage = "Format dışı email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        public string PrimaryContactUserNameSurname { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        public string PrimaryContactUserEmail { get; set; }
        public bool EffortApproval { get; set; }
        public int EffortApprovalLimit { get; set; }
        public string EffortApprovalTemporary { get; set; }

    }
}
