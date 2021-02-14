using Proton_Server_Core.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Identity
{
    public static class Account
    {
        public static bool Create(string email, string password)
        {
            return AccountsDB.CreateAccount(email, password).GetAwaiter().GetResult();
        }
    }
}
