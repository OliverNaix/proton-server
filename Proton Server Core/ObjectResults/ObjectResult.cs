using Proton_Server_Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.ObjectResults
{
    public class ObjectResult : IActionResult
    {
        public string Type { get; set; }
        public object Object { get; set; }
        public int StatusCode { get; set; }
        public ObjectResult(string type, object value)
        {
            this.Type = type;
            this.Object = value;
        }
    }
}
