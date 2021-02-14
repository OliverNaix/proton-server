using Proton_Server_Core.Network;
using Proton_Server_Core.ObjectResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Actions
{
    public abstract class Action
    {
        public abstract string Type { get; }
        public abstract ObjectResult Run(Client client, object value);
    }
}
