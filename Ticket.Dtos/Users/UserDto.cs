using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ticket.Dtos.Users
{
    public class UserDto : BaseDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        [EmailAddress(ErrorMessage = "Format dışı email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        [MaxLength(11, ErrorMessage = "Tc No 11 haneli olmalıdır"), MinLength(11, ErrorMessage = "Tc No 11 haneli olmalıdır")]
        public string IdentityNumber { get; set; }
        [Required(ErrorMessage = "Zorunlu alan")]
        [Phone(ErrorMessage = "Format dışı gsm")]
        public string Gsm { get; set; }
        public string Password { get; set; }
    }
}
