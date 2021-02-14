using Proton_Server_Core.Interfaces;
using Proton_Server_Core.ObjectResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proton_Server_Core.Controllers
{
    public abstract class Controller
    {
        public virtual OkObjectResult Ok(string type, object value)
        {
            return new OkObjectResult(type, value);
        }
        public virtual string GetName()
        {
            return this.GetType().ToString().Split('.').Last();
        }
        public virtual BadObjectResult BadRequest(string type, object value)
        {
            return new BadObjectResult(type, value);
        }
    }
}
