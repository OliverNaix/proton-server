using Proton_Server_Core.Controllers;
using Proton_Server_Core.Identity;
using Proton_Server_Core.Network;
using Proton_Server_Core.ObjectResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Actions
{
    class LoginAction : Action
    {
        public override string Type
        {
            get => "Auhtorization-Email";
        }

        public override ObjectResult Run(Client client, object value)
        {
            if (!Email.IsValid(value))
            {
                return new BadObjectResult("Email address is empty or invalid", false);
            }

            client.ClientData.Email = value.ToString();

            return Email.Exists(value.ToString()) ? new OkObjectResult("login_action", true) : new OkObjectResult("login_action", false);
        }
    }
}
