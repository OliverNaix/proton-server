using Microsoft.AspNetCore.Mvc;
using Proton.DatabaseContext;

namespace Proton.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DialogsController : ControllerBase
    {
        private readonly AccountDbContext _accountDbContext;
        public DialogsController(AccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        [HttpPost]
        public ObjectResult Post(string token)
        {
            return Ok("");
        }
    }
}
