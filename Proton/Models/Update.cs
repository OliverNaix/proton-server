using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proton.Models
{
    public class Update
    {
        public string Type { get; set; }
        public object Object { get; set; }
        public string Token { get; set; }
        public bool OperationComplete { get; set; }
    }
}
