using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Ticket.Helper
{
    public class HashService : IHashService
    {
        public string CreateHash(string value, string salt)
        {
            try
            {
                /*
                 * Gönderilen value gönderilen salt key'e göre hash işlemi yapılır
                 * using Microsoft.AspNetCore.Cryptography.KeyDerivation;
                */
                var values = KeyDerivation.Pbkdf2(password: value, Encoding.UTF8.GetBytes(salt), prf: KeyDerivationPrf.HMACSHA512, iterationCount: 10000, numBytesRequested: 256 / 8);
                return Convert.ToBase64String(values) + "æ" + salt;
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public string CreateSalt()
        {
            /*
             * Her işlem için bir salt oluşturulur
             */

            byte[] random = new byte[128 / 8];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        public bool ValidateHash(string value, string salt, string hash)
        {
            /*
             * Gelen veri hash edilir daha sonra başka bir  hash edilmiş veri ile  karşılaştırılır 
             */
            var result = CreateHash(value, salt);
            return result == hash.Split("æ")[0] ? true : false;

        }
    }
}
