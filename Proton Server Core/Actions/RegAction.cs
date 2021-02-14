using Proton_Server_Core.Database;
using Proton_Server_Core.Identity;
using Proton_Server_Core.Network;
using Proton_Server_Core.ObjectResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Actions
{
    class RegAction : Action
    {
        public override string Type
        {
            get => "Registration";
        }
        public override ObjectResult Run(Client client, object value)
        {
            if (string.IsNullOrEmpty(client.ClientData.Email) || !Email.IsValid(client.ClientData.Email))
            {
                return new BadObjectResult("Invalid Email address!", false);
            }

            if (AccountsDB.EmailExists(client.ClientData.Email).Result)
            {
                return new BadObjectResult("Account already exists", false);
            }

            if (!Password.IsValid(value))
            {
                return new BadObjectResult("Bad password", false);
            }

            var result = Account.Create(client.ClientData.Email, value.ToString());

            return result ? new OkObjectResult("password_action", result) : new OkObjectResult("password_action", result);
        }
    }
}
