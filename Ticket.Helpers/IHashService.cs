using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Helper
{
    public interface IHashService
    {
        string CreateHash(string value, string salt);
        string CreateSalt();
        bool ValidateHash(string value, string salt, string hash);
    }
}
