using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Models
{
    public class Dialog
    {
        public List<User> Users { get; set; }
        public DateTime Created { get; set; }
    }
}
