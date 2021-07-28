using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ticket.Helper
{
    public class EncryptionService : IEncryptionService
    {

        IDataProtectionProvider _protectionProvider;
        public EncryptionService(IDataProtectionProvider protectionProvider)
        {
            _protectionProvider = protectionProvider;
        }
        const string key = "@2021@Ticket@2021";
        public string Decrypt(string input)
        {

            if (input == null)
                throw new ArgumentNullException(nameof(input));
            else
            {
                var unprotect = _protectionProvider.CreateProtector(key);
                return unprotect.Protect(input);
            }
        }
        public string Encrypt(string input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input));
            else
            {
                var protect = _protectionProvider.CreateProtector(key);
                return protect.Protect(input);
            }
        }
    }
}
