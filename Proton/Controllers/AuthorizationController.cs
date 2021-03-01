using Microsoft.AspNetCore.Mvc;
using Proton.DatabaseContext;
using Proton.Models;


namespace Proton.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : ControllerBase
    {
        private readonly AccountDbContext _accountDbContext;
        public AuthorizationController(AccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }


        [HttpPost] 
        public ObjectResult Post(LoginBindingModel loginBindingModel)
        {
            Update update = new Update();

            int id = _accountDbContext.AccountExists(loginBindingModel.Email, loginBindingModel.Password);

            if (id != 0)
            {
                update.Type                 = "Authorization";
                update.Object               = _accountDbContext.FindAccountById(id);
                update.OperationComplete    = true;
                update.Token                = Identity.Identity.GetToken();

                if (update.Object != null)
                {
                    Identity.Identity.AddToken(update.Token, update.Object as Account);
                }
                else
                {
                    return BadRequest(update);
                }
            }

            return Ok(update);
        }
    }
}
