using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.ObjectResults
{
    public class BadObjectResult : ObjectResult
    {
        public BadObjectResult(string type, object value) : base(type, value)
        {
            this.Type = type;
            this.Object = value;
            this.StatusCode = 400;
        }
    }
}
