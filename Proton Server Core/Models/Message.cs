using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Models
{
    public class Message
    {
        public int Id { get; }
        public User To { get; set; }
        public string Text { get; set; }
        public string Time { get; set; }
    }
}
