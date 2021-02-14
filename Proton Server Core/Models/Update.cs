using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Controllers
{
    public class Update
    {
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("object")]
        public object Object { get; set; }
    }
}
