using Proton.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Proton.Identity
{
    public class Identity
    {
        public static Dictionary<string, Account> TokenUsers { get; set; } = new Dictionary<string, Account>();
        private static RandomNumberGenerator RandomNumberGenerator = RandomNumberGenerator.Create();
        public static string GetToken()
        {
            byte[] array = new byte[1024];

            RandomNumberGenerator.GetNonZeroBytes(array);

            return Convert.ToBase64String(array);
        }

        public static void AddToken(string token, Account account)
        {
            TokenUsers.Add(token, account);
        }
    }
}
