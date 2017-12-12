using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EamaShop.Identity.API.Controllers
{
    [Route("authorize")]
    public class OAuthController : Controller
    {
        [HttpGet("connect/oauth")]
        public IActionResult Index(string redirect_uri,string client_id,string scope,string state)
        {
            return View();
        }
    }
}