using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EamaShop.Delievery.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => Redirect("~/swagger");
    }
}
