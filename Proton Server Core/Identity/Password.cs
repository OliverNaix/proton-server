using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Identity
{
    public static class Password
    {

        public static bool IsValid(object passwordObj)
        {
            if (passwordObj == null)
            {
                return false;
            }

            string password = passwordObj.ToString();

            return (!string.IsNullOrEmpty(password) || !string.IsNullOrWhiteSpace(password)) && password.Length > 8 ;
        }
    }
}
