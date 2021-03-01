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
    public class SearchController : ControllerBase
    {
        private readonly AccountDbContext _accountDbContext;
        public SearchController(AccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        [HttpPost]
        public ObjectResult Post(SearchBindngModel searchBindngModel)
        {
            return Ok(_accountDbContext.FindAccountsByEmail(searchBindngModel.Email));
        }
    }
}
