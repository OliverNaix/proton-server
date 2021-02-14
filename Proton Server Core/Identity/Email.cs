using Proton_Server_Core.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Identity
{
    public static class Email
    {
        public static bool Exists(string email)
        {
            return AccountsDB.EmailExists(email).Result;
        }

        public static bool IsValid(object emailObj)
        {
            if (emailObj == null)
            {
                return false;
            }

            string email = emailObj.ToString();

            if (string.IsNullOrEmpty(email) || string.IsNullOrWhiteSpace(email))
            {
                return false;   
            }

            if (!email.Contains('@') || !email.Contains('.'))
            {
                return false;
            }

            if (email.Length <= 5)
            {
                return false;
            }

            return true;
        }
    }
}
