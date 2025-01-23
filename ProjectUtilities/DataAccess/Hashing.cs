using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;

namespace ProjectUtilities.DataAccess
{
    public class Hashing: IHashing
    {
        public String HashPassword(String password)
        {
            var HashedPassword = "";

            if (!(string.IsNullOrEmpty(password)))
            {
                HashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

            }

            return HashedPassword;
        }

        public bool VerifyPassword(String Password, String hashedPassword)
        {
            bool result = false;

            if (!string.IsNullOrEmpty(hashedPassword) && !string.IsNullOrEmpty(Password))
            {
                result = BCrypt.Net.BCrypt.Verify(Password, hashedPassword);
            }

            return result;
        }
    }
}
