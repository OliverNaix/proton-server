using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Models
{
    public class User
    {
        public string Username { get; set; }
        public string LastSeen { get; set; }
        public bool IsFriend { get; set; }
    }
}
