using Proton_Server_Core.Database;
using Proton_Server_Core.Identity;
using Proton_Server_Core.Network;
using Proton_Server_Core.ObjectResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Actions
{
    class PasswordAction : Action
    {
        public override string Type
        {
            get => "Auhtorization-Password";
        }

        public override ObjectResult Run(Client client, object value)
        {
            if (String.IsNullOrEmpty(client.ClientData.Email))
            {
                return new BadObjectResult("Invalid Email address", false);
            }

            if (!Password.IsValid(value))
            {
                return new BadObjectResult("Not a valid password!", false);
            }

            if (AccountsDB.AccountExists(client.ClientData.Email, value.ToString()).Result)
            {
                return new OkObjectResult(Type, true);
            }
            else
            {
                return new OkObjectResult(Type, false); 
            }
        }
    }
}
