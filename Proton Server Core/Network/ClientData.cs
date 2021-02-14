using Proton_Server_Core.Database;

namespace Proton_Server_Core.Network
{
    public class ClientData
    {
        public string Email { get; set; }
        public string Username { get; set; }
        private int id;
        public int Id
        {
            get
            {
                if (id == 0)
                {
                    id = AccountsDB.GetAccountId(Email).Result;
                }

                return id;
            }
        }
    }
}
