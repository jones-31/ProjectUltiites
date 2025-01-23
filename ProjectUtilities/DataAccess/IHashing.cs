using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectUtilities.DataAccess
{
    public interface IHashing
    {
        /// <summary>
        /// Hashes the given password using BCrypt .
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Returns the hashed password.</returns>
        public String HashPassword(String password);

        /// <summary>
        /// Checks if the given Password and the hashedPassword matches .
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="hashedPassword"></param>
        /// <returns>Returns True if the Password and hashedPassword matches else returns False</returns>
        public bool VerifyPassword(String Password, String hashedPassword);
    }
}
