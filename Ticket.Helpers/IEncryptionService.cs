using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Helper
{
    public interface IEncryptionService
    {
        string Encrypt(string input);
        string Decrypt(string input);
    }
}
