using Proton_Server_Core.Controllers;
using Proton_Server_Core.Network;
using Proton_Server_Core.ObjectResults;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core.Actions
{
    public static class Actions
    {
        public static Dictionary<string, Action> ActionDictionary = new Dictionary<string, Action>();

        public static void Add(Action action)
        {
            ActionDictionary.Add(action.Type, action);
        }

        public static void Init()
        {
            Add(new RegAction());

            Add(new LoginAction());

            Add(new PasswordAction());
        }

        public static ObjectResult Run(Client client, Update updates)
        {
            client.LastActionType = updates.Type;

            if (ActionDictionary.ContainsKey(updates.Type))
            {
                return ActionDictionary[updates.Type].Run(client, updates.Object);
            }
            else
            {
                Console.WriteLine($"ActionDictionary не содержи ключ: {updates.Type}");
            }

            return new BadObjectResult("", $"ActionDictionary does not contains {updates.Type} key");
        }
    }
}
