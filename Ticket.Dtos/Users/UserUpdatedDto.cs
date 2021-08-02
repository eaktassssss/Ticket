﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Dtos.Users
{
    public class UserUpdatedDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string IdentityNumber { get; set; }
        public string Gsm { get; set; }
        public string Password { get; set; }
    }
}
