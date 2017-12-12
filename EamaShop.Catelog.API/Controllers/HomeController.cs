using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EamaShop.Catelog.API.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index() => Redirect("~/swagger");
    }
}
