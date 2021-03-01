using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proton.Models
{
    public class RegisterBindingModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Address { get; set; }
        public string Datetime { get; set; }
    }
}
