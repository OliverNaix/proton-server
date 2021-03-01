using Microsoft.AspNetCore.Mvc;
using Proton.DatabaseContext;
using Proton.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proton.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private readonly AccountDbContext _accountDbContext;
        public RegistrationController(AccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public ObjectResult Post(RegisterBindingModel registerBindingModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model state");
            }

            if (_accountDbContext.AccountExists(registerBindingModel.Email))
            {
                return Ok("Account is already exists.");
            }

            registerBindingModel.Address    = HttpContext.Connection.RemoteIpAddress.ToString();
            registerBindingModel.Datetime   = DateTime.Now.ToString();

            _accountDbContext.CreateAccount(registerBindingModel);

            return Ok("Account successfully created!");
        }
    }
}
