using Proton_Server_Core.Controllers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Proton_Server_Core
{
    public static class Services
    {
        public static Dictionary<string, Controller> ServiceControllers { get; set; }
        public static void Add(Controller controller)
        {
            if (ServiceControllers == null)
            {
                ServiceControllers = new Dictionary<string, Controller>();
            }

            ServiceControllers.Add(controller.GetName(), controller);
        }
    }
}
